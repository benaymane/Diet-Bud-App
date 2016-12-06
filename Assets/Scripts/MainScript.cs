using UnityEngine;
using System;
using UnityEngine.UI;
using System.Collections.Generic;

public class MainScript : MonoBehaviour {
    public Text caloriesValue,
        fatValue,
        proteinValue,
        carbsValue;

    public Dropdown meals_DD;

    public InputField serving;

    public string[][] meals;

    private DataHandler data = new DataHandler( );

	// Use this for initialization
	void Start ( ) {
        init_cals( );

        init_meals( );

	}

    public void addCals( ) {
        int value = meals_DD.value;

        int serving = Int32.Parse( this.serving.text );
        print( meals[ value ][ DataHandler.CALORIES_INDEX + 1] );
        data.setCalories( meals[value][DataHandler.CALORIES_INDEX + 1] );
        data.setFat( meals[ value ][ DataHandler.FAT_INDEX + 1] );
        data.setProtein( meals[ value ][ DataHandler.PROTEIN_INDEX + 1] );
        data.setCarbs( meals[ value ][ DataHandler.CARBS_INDEX + 1] );

        init_cals( );
    }

    //Iitialises the calorie count text.
    void init_cals( ) {
        caloriesValue.text = data.getCalories( ).ToString( );
        fatValue.text = data.getFat( ).ToString( );
        proteinValue.text = data.getProtein( ).ToString( );
        carbsValue.text = data.getCarbs( ).ToString( );
    }

    void init_meals( ) {
        meals = data.readAllMeals( );

        for( int i = 0; i < DataHandler.curr_meals_size; i++ ) {
           meals_DD.options.Add( new Dropdown.OptionData( meals[ i ][ 0 ] ) );
        }
    }
}
