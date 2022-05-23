using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class SettingsManager : MonoBehaviour {

	[Header("Options Lists")]
	public Slider lookSensitivity;
	public TMP_Text sensitivityText;

	[Header("Visual Settings")]
	public TMP_Text textQualityPreset;
	public TMP_Text textTexturePreset;
	public TMP_Text textShadowQuality;
	public TMP_Text textShadowCascades;
	public TMP_Text textLightDetail;
	public TMP_Text textParticleFxQuality;
	public List<string> qualityPreset = new List<string>();
	public List<string> textureQuality = new List<string>();
	public List<string> shadowQuality = new List<string>();
	public List<string> shadowCascades = new List<string>();
	public List<string> lightDetail = new List<string>();
	public List<string> particleFxQuality = new List<string>();

	[Header("Starting Options Values")]
	public int qualityPresetDefault = 0;
	public int textureQualityDefault = 0;
	public int shadowQualityDefault = 0;
	public int shadowCascadesDefault = 0;
	public int lightDetailDefault = 0;
	public int particleFxQualityDefault = 0;

	[Header("List Indexing")]
	int qualityPresetIndex = 0;
	int textureQualityIndex = 0;
	int shadowQualityIndex = 0;
	int shadowCascadesIndex = 0;
	int lightDetailIndex = 0;
	int particleFxQualityIndex = 0;

	void Start () {
		qualityPresetIndex = qualityPresetDefault;
		textureQualityIndex = textureQualityDefault;
		shadowQualityIndex = shadowQualityDefault;
		shadowCascadesIndex = shadowCascadesDefault;
		lightDetailIndex = lightDetailDefault;
		particleFxQualityIndex = particleFxQualityDefault;

		textQualityPreset.text = qualityPreset[qualityPresetDefault];
		textTexturePreset.text = textureQuality[textureQualityDefault];
		textShadowQuality.text = shadowQuality[shadowQualityDefault];
		textShadowCascades.text = shadowCascades[shadowCascadesDefault];
		textLightDetail.text = lightDetail[lightDetailDefault];
		textParticleFxQuality.text = particleFxQuality[particleFxQualityDefault];
	}
	
	public void IncreaseIndex(int i){
		switch (i){
			case 0:
				if(qualityPresetIndex != qualityPreset.Count -1){qualityPresetIndex++;}else{qualityPresetIndex = 0;}
				textQualityPreset.text = qualityPreset[qualityPresetIndex];
				break;
			case 1:
				if(textureQualityIndex != textureQuality.Count -1){textureQualityIndex++;}else{textureQualityIndex = 0;}
				textTexturePreset.text = textureQuality[textureQualityIndex];
				break;
			case 2:
				if(shadowQualityIndex != shadowQuality.Count -1){shadowQualityIndex++;}else{shadowQualityIndex = 0;}
				textShadowQuality.text = shadowQuality[shadowQualityIndex];
				break;
			case 3:
				if(shadowCascadesIndex != shadowCascades.Count -1){shadowCascadesIndex++;}else{shadowCascadesIndex = 0;}
				textShadowCascades.text = shadowCascades[shadowCascadesIndex];
				break;
			case 4:
				if(lightDetailIndex != lightDetail.Count -1){lightDetailIndex++;}else{lightDetailIndex = 0;}
				textLightDetail.text = lightDetail[lightDetailIndex];
				break;
			case 5:
				if(particleFxQualityIndex != particleFxQuality.Count -1){particleFxQualityIndex++;}else{particleFxQualityIndex = 0;}
				textParticleFxQuality.text = particleFxQuality[particleFxQualityIndex];
				break;
		}
	}

	public void DecreaseIndex(int i){
		switch (i){
			case 0:
				if(qualityPresetIndex == 0){qualityPresetIndex = qualityPreset.Count;}qualityPresetIndex--;
				textQualityPreset.text = qualityPreset[qualityPresetIndex];
				break;
			case 1:
				if(textureQualityIndex == 0){textureQualityIndex = textureQuality.Count;}textureQualityIndex--;
				textTexturePreset.text = textureQuality[textureQualityIndex];
				break;
			case 2:
				if(shadowQualityIndex == 0){shadowQualityIndex = shadowQuality.Count;}shadowQualityIndex--;
				textShadowQuality.text = shadowQuality[shadowQualityIndex];
				break;
			case 3:
				if(shadowCascadesIndex == 0){shadowCascadesIndex = shadowCascades.Count;}shadowCascadesIndex--;
				textShadowCascades.text = shadowCascades[shadowCascadesIndex];
				break;
			case 4:
				if(lightDetailIndex == 0){lightDetailIndex = lightDetail.Count;}lightDetailIndex--;
				textLightDetail.text = lightDetail[lightDetailIndex];
				break;
			case 5:
				if(particleFxQualityIndex == 0){particleFxQualityIndex = particleFxQuality.Count;}particleFxQualityIndex--;
				textParticleFxQuality.text = particleFxQuality[particleFxQualityIndex];
				break;
		}
	}

	public void UpdateSensitivity(){
		sensitivityText.text = "" + lookSensitivity.value.ToString("F2");
	}
}
