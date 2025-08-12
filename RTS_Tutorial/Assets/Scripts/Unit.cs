using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Unit : MonoBehaviour
{
  public int unitHealth;
  public int unitMaxHealth;
  public HealthTracker healthTracker;
  void Start()
  {
    UnitSelectionManager.Instance.allUnitsList.Add(gameObject);

    unitHealth = unitMaxHealth;
    UpdateHealthUI();
  }



  private void OnDestroy()
  {
    UnitSelectionManager.Instance.allUnitsList.Remove(gameObject);
  }
  private void UpdateHealthUI()
  {
    healthTracker.UpdateSliderValue(unitHealth, unitMaxHealth);

    if (unitHealth <= 0)
    {
      Destroy(gameObject);
    }
  }
  public void TakeDamage(int damageToInflict)
  {
    unitHealth -= damageToInflict;
    UpdateHealthUI();
  }

}
