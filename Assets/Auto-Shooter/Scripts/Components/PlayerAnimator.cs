using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class PlayerAnimator
{
	public Animator animator;
	public bool IsConfigured;
	#region Animations
	private int MoveX;
	private int MoveY;
	private int IsGrounded;
	private int IsJumping;
	private int IsFalling;
	private int IsRunning;
	private int IsDead;
	#endregion
	public void GenerateHash()
	{
		MoveX = Animator.StringToHash("MoveX");
		MoveY = Animator.StringToHash("MoveY");
		IsJumping = Animator.StringToHash("Jump");
		IsFalling = Animator.StringToHash("IsFalling");
		IsGrounded = Animator.StringToHash("IsGrounded");
		IsRunning = Animator.StringToHash("IsRunning");
		IsDead = Animator.StringToHash("IsDead");

		IsConfigured = true;
	}

	public void SetMovement(Vector2 movement)
	{
		if (!IsConfigured) return;
		animator.SetFloat(MoveX, movement.x);
		animator.SetFloat(MoveY, movement.y);
	}

	public void SetJumping(bool IsJump)
	{
		if (!IsConfigured) return;
		animator.SetBool(IsJumping, IsJump);
	}

	public void SetFall(bool IsFalling)
	{
		if (!IsConfigured) return;
		animator.SetBool(this.IsFalling, IsFalling);
	}

	public void SetGrounded(bool IsGrounded)
	{
		if (!IsConfigured) return;
		animator.SetBool(this.IsGrounded, IsGrounded);
	}

	public void SetRunning(bool IsRunning)
	{
		if (!IsConfigured) return;
		animator.SetBool(this.IsRunning, IsRunning);
	}

	public void Play(string anim)
	{
		animator.Play(anim);
	}
}
