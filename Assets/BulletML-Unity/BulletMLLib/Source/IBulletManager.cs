using UnityEngine;

namespace BulletMLLib
{
  /// <summary>
  /// This is the interface that outisde assemblies will use to manage bullets... mostly for creating/destroying them
  /// </summary>
  public interface IBulletManager
  {
    #region Methods

    /// <summary>
    /// a mathod to get current position of the player
    /// This is used to target bullets at that position
    /// </summary>
    /// <returns>The position to aim the bullet at</returns>
    /// <param name="targettedBullet">the bullet we are getting a target for</param>
    Vector2 PlayerPosition(Bullet targettedBullet);

    /// <summary>
    /// A bullet is done being used, do something to get rid of it.
    /// </summary>
    /// <param name="deadBullet">the Dead bullet.</param>
    void RemoveBullet(Bullet deadBullet);

    /// <summary>
    /// Create a new bullet.
    /// </summary>
    /// <param name="source">Bullet requesting a new friend</param>
    /// <param name="top">Is this bullet the root of a top action?</param>
    /// <returns>A shiny new bullet</returns>
    Bullet CreateBullet(Bullet source, bool top);

    /// <summary>
    /// Raise a trigger
    /// </summary>
    /// <param name="source">Bullet requesting an action</param>
    /// <param name="name">Trigger's name</param>
    void Trigger(Bullet source, string name);

    #endregion //Methods
  }
}
