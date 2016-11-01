﻿using UnityEngine;
using System;

namespace RobotStuff
{
	public class RobotStrategyC : RobotStrategy
	{
		public RobotStrategyC(GameObject gameObject, Transform target) : base(gameObject, target)
		{
			Debug.Log("New RobotC");
		}

		public override void Start()
		{
			_followRange = 500.0f;
			_arriveThreshold = 0.05f;
			_followSpeed = 2.0f;
		}

		public override void Update()
		{
			FollowTarget();
		}

		public void FollowTarget()
		{
			if(_target != null)
			{
				Vector3 direction = _target.transform.position - _myGameObject.transform.position;
				if(direction.magnitude <= _followRange)
				{
					_myGameObject.transform.rotation = Quaternion.RotateTowards(_myGameObject.transform.rotation, Quaternion.LookRotation(direction), _angularSpeed * Time.deltaTime);

					if(direction.magnitude > _arriveThreshold)
					{
						_myGameObject.transform.Translate(direction.normalized * _followSpeed * Time.deltaTime, Space.World);
					}
					else
					{
						_myGameObject.transform.position = _target.transform.position;
					}
				}
			}
		}
	}
}

