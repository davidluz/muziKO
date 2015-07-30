using UnityEngine;
using System.Collections;

public class ControladorQuests {

	[System.Serializable]
	public struct questChain{
		public string chainName;
		public questSteps[] chainSteps;

		[HideInInspector]
		public bool chainComplete;
		[HideInInspector]
		public bool chainActive;
		[HideInInspector]
		public int chainStep;

		public questChain(int chainStepNumber){
			chainName = "";
			chainSteps = new questSteps[chainStepNumber];
			chainComplete = false;
			chainActive = false;
			chainStep = 0;
		}
	}

	[System.Serializable]
	public struct questSteps{
		public string stepName;
		public int stepQtdeDeliver;
		public Transform stepTarget;

		[HideInInspector]
		public bool stepComplete;
		[HideInInspector]
		public bool stepActive;

		public questSteps(string stepCurrName){
			stepName = stepCurrName;
			stepQtdeDeliver = 0;
			stepTarget = null;
			stepComplete = false;
			stepActive = false;
		}
	}

}
