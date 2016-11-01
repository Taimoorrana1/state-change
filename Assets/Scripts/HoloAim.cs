﻿using UnityEngine;
using System.Collections;

public class HoloAim : MonoBehaviour
{
    public GameObject holoBlock;
    public GameObject aimer;

    private PlayerExtension parentPlayer;
    [SerializeField] private float m_ScaleSpeed;
    private GameObject highlightedBlock;
	private bool targetting;
    
    void Start ()
    {
        parentPlayer = transform.parent.transform.parent.gameObject.GetComponent<PlayerExtension>();
		holoBlock.SetActive (false);
		targetting = false;
    }
	
	// Update is called once per frame
	void Update ()
    {
        RaycastHit hit;
		if (Input.GetButtonDown("ActivateAiming"))
        {
			targetting = !targetting;
			holoBlock.SetActive (targetting);
        }

        if (targetting)
        {
            if (Physics.Raycast(transform.position, aimer.transform.forward, out hit) && hit.transform.gameObject.layer == LayerMask.NameToLayer("Changeable")) //draw line from pos in the fwrd direction, store collision info in "hit"
            {
                if (parentPlayer.getCharge() == 0)
                    holoBlock.transform.localScale = new Vector3(1.0f, 1.0f, 1.0f);
                else
                    holoBlock.transform.localScale = new Vector3(1.0f, 1.0f + (parentPlayer.getCharge() * m_ScaleSpeed), 1.0f);
                holoBlock.transform.position = hit.point;
                holoBlock.transform.rotation = hit.transform.gameObject.transform.rotation;
                if (hit.transform.gameObject != highlightedBlock)
                {
                    if (highlightedBlock != null)
                        highlightedBlock.GetComponent<Renderer>().material.color = Color.red;
                    highlightedBlock = null;
                }
            }
            else if (Physics.Raycast(transform.position, aimer.transform.forward, out hit) && hit.transform.gameObject.layer == LayerMask.NameToLayer("MassBlock"))
            {
				if (hit.transform.gameObject.GetComponent<Renderer>())
                	hit.transform.gameObject.GetComponent<Renderer>().material.color = Color.green;
                highlightedBlock = hit.transform.gameObject;
                if (hit.transform.gameObject != highlightedBlock)
                {
                    highlightedBlock.GetComponent<Renderer>().material.color = Color.red;
                    highlightedBlock = hit.transform.gameObject;
                }
            }
        }
    }
}
