﻿using System.Diagnostics;

namespace BulletMLLib
{
  /// <summary>
  /// This task changes the speed a little bit every frame.
  /// </summary>
  public class ChangeSpeedTask : BulletMLTask
  {
    #region Members

    /// <summary>
    /// The amount to change speed every frame
    /// </summary>
    private float SpeedChange { get; set; }

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
    public ChangeSpeedTask(ChangeSpeedNode node, BulletMLTask owner) : base(node, owner)
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
      //set the length of time to run this dude
      startDuration = Node.GetChildValue(ENodeName.term, this);

      //check for divide by 0
      if (0.0f == startDuration)
      {
        startDuration = 1.0f;
      }

      float ratio = TimeFix.Framerate / 60f;
      startDuration *= ratio;

      Duration = startDuration;

      switch (Node.GetChild(ENodeName.speed).NodeType)
      {
        case ENodeType.sequence:
          {
            SpeedChange = Node.GetChildValue(ENodeName.speed, this);
          }
          break;

        case ENodeType.relative:
          {
            SpeedChange = Node.GetChildValue(ENodeName.speed, this) / Duration;
          }
          break;

        default:
          {
            SpeedChange = (Node.GetChildValue(ENodeName.speed, this) - bullet.Speed) / Duration;
          }
          break;
      }
    }

    /// <summary>
    /// Run this task and all subtasks against a bullet
    /// This is called once a frame during runtime.
    /// </summary>
    /// <returns>ERunStatus: whether this task is done, paused, or still running</returns>
    /// <param name="bullet">The bullet to update this task against.</param>
    public override ERunStatus Run(Bullet bullet)
    {
      bullet.Speed += SpeedChange;

      Duration -= 1.0f * bullet.TimeSpeed;
      if (Duration <= 0.0f || startDuration <= 1)
      {
        TaskFinished = true;
        return ERunStatus.End;
      }
      else
      {
        return ERunStatus.Continue;
      }
    }

    #endregion //Methods
  }
}