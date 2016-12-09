using UnityEngine;
using System;
using UnityEngine.UI;
using System.Collections.Generic;

public class MainScript : ErrorHandler {
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
        error = false;
        int mealValue = meals_DD.value;

        if( mealValue == 0 )
            sendError( errorCode.INVALID_MEAL );

        if( error )
            return;

        int servingValue = ( serving.text == "" ) ? 1 : Int32.Parse( this.serving.text );

        mealValue--;

        data.setCalories( meals[ mealValue ][DataHandler.CALORIES_INDEX + 1], servingValue );
        data.setFat( meals[ mealValue ][ DataHandler.FAT_INDEX + 1], servingValue );
        data.setProtein( meals[ mealValue ][ DataHandler.PROTEIN_INDEX + 1], servingValue );
        data.setCarbs( meals[ mealValue ][ DataHandler.CARBS_INDEX + 1], servingValue );

        init_cals( );

        sendGood( meals[ mealValue ][ 0 ] + " has been counted in! " );
    }

    public void clearAll( ) {
        data.resetCals( );
        sendGood( "All cleared!" );
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
        
        for( int i = 0; i < data.curr_meals_size; i++ ) {
            if( meals [i] == null ) {
                print( i );
                return;
            }
           meals_DD.options.Add( new Dropdown.OptionData( meals[ i ][ 0 ] ) );
        }
    }
}
