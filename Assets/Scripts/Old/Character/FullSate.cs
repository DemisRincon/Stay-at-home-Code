using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FullSate
{
    [SerializeField] private State energy = new State(100);
    [SerializeField] private State social = new State(100);
    [SerializeField] private State satiety = new State(100);
    [SerializeField] private State thirst = new State(100);
    [SerializeField] private State hygiene = new State(100);
    [SerializeField] private State fun = new State(100);
    [SerializeField] private State stress = new State(100,true);
    [SerializeField] private State happiness = new State(100);
    [SerializeField] private State weight = new State(80);
    public bool brakefast { get; private set; }
    public bool bath { get; private set; }
    public bool work { get; private set; }
    public bool clean { get; private set; }
    public bool eat { get; private set; }
    public bool dinner { get; private set; }
    public bool sleep { get; private set; }


    public FullSate()
    {
        brakefast = false;
        bath = false;
        work = false;
        clean = false;
        eat = false;
        dinner = false;
        sleep = false;
    }

    public State GetEnergy()
    {
        return energy;
    }
    public State GetSocial()
    {
        return social;
    }
    public State GetSatiety()
    {
        return satiety;
    }
    public State GetThirst()
    {
        return thirst;
    }
    public State GetHygiene()
    {
        return hygiene;
    }
    public State GetFun()
    {
        return fun;
    }
    public State GetStress()
    {
        return stress;
    }
    public State GetHappiness()
    {
        return happiness;
    }
    public State GetWeight()
    {
        return weight;
    }


    public void Brakefast() { 
        brakefast = !brakefast; 
    }
    public void Bath() {
        bath = !bath;
    }
    public void Work() {
        work = !work;
    }
    public void Clean() {
        clean = !clean;
    }
    public void Eat() {
        eat = !eat;
    }
    public void Dinner() {
        dinner = !dinner;
    }
    public void Sleep() {
        sleep = !sleep;
    }
}
