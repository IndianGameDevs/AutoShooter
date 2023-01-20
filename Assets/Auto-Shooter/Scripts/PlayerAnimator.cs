using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimator : MonoBehaviour
{
	public Animator animator;

    #region Animations
    private int moveX;
	private int moveY;
	private int fall;
	private int land;
	private int jump;
	private int IsRunning;
    #endregion
    public void GenerateHash()
	{
		moveX = Animator.StringToHash("MoveX");
		moveY = Animator.StringToHash("MoveY");
		IsRunning = Animator.StringToHash("IsRunning");
	}

	public void SetMovement(Vector2 movement)
	{
		animator.SetFloat(moveX, movement.x);
		animator.SetFloat(moveY, movement.y);
	}

	public void SetRunning(bool IsRunning)
	{
		animator.SetBool(this.IsRunning, IsRunning);
	}
}
