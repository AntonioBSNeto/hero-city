﻿using System;
using System.Diagnostics;
using UnityEngine;

namespace BulletMLLib
{
	/// <summary>
	/// This task changes the direction a little bit every frame
	/// </summary>
	public class ChangeDirectionTask : BulletMLTask
	{
		#region Members

		/// <summary>
		/// The amount to change driection every frame
		/// </summary>
		private float DirectionChange;

		/// <summary>
		/// How long to run this task... measured in frames
		/// </summary>
		private float Duration { get; set; }

    private float startDuration;

		#endregion //Members

		#region Methods

		/// <summary>
		/// Initializes a new instance of the <see cref="BulletMLLib.BulletMLTask"/> class.
		/// </summary>
		/// <param name="node">Node.</param>
		/// <param name="owner">Owner.</param>
		public ChangeDirectionTask(ChangeDirectionNode node, BulletMLTask owner) : base(node, owner)
		{
			System.Diagnostics.Debug.Assert(null != Node);
			System.Diagnostics.Debug.Assert(null != Owner);
		}

		/// <summary>
		/// this sets up the task to be run.
		/// </summary>
		/// <param name="bullet">Bullet.</param>
		protected override void SetupTask(Bullet bullet)
		{
      //set the time length to run this dude
      startDuration = Node.GetChildValue(ENodeName.term, this);

      //check for divide by 0
      if (0.0f == startDuration)
      {
        startDuration = 1.0f;
      }

      // Remove the 60 FPS limit (or at least try, ChangeDirection is very, very sensitive to frame variation)
      float ratio = TimeFix.Framerate / 60f;

      startDuration *= ratio;

      Duration = startDuration;

      //Get the amount to change direction from the nodes
      DirectionNode dirNode = Node.GetChild(ENodeName.direction) as DirectionNode;
			float value = dirNode.GetValue(this) * (float)Mathf.PI / 180.0f; //also make sure to convert to radians

			//How do we want to change direction?
			ENodeType changeType = dirNode.NodeType;
			switch (changeType)
			{
				case ENodeType.sequence:
				{
					//We are going to add this amount to the direction every frame
					DirectionChange = value;
				}
				break;

				case ENodeType.absolute:
				{
					//We are going to go in the direction we are given, regardless of where we are pointing right now
					DirectionChange = value - bullet.Direction;
				}
				break;

				case ENodeType.relative:
				{
					//The direction change will be relative to our current direction
					DirectionChange = value;
				}
				break;

				default:
				{
					//the direction change is to aim at the enemy
					DirectionChange = ((value + bullet.GetAimDir()) - bullet.Direction);
				}
				break;
			}

			//keep the direction between 0 and 360
			if (DirectionChange > Mathf.PI)
			{
				DirectionChange -= 2 * (float)Mathf.PI;
			}
			else if (DirectionChange < -Mathf.PI)
			{
				DirectionChange += 2 * (float)Mathf.PI;
			}

			//The sequence type of change direction is unaffected by the duration
			if (changeType != ENodeType.sequence)
			{
				//Divide by the duration so we ease into the direction change
				DirectionChange /= Duration;
			}
		}
		
		public override ERunStatus Run(Bullet bullet)
		{
			//change the direction of the bullet by the correct amount
			bullet.Direction += DirectionChange;

			//decrement the amount if time left to run and return End when this task is finished
			Duration -= 1.0f * bullet.TimeSpeed;
			if (Duration <= 0.0f || startDuration <= 1)
			{
				TaskFinished = true;
				return ERunStatus.End;
			}
			else
			{
				//since this task isn't finished, run it again next time
				return ERunStatus.Continue;
			}
		}

		#endregion //Methods
	}
}