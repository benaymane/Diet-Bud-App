using UnityEngine;
using System;
using UnityEngine.UI;
using System.Collections.Generic;

public class MainScript : ErrorHandler {

    public static DataHandler data;

    public Text caloriesValue,
        fatValue,
        proteinValue,
        carbsValue;

    public Dropdown meals_DD;

    public InputField serving;

    public string[][] meals;

	// Use this for initialization
	void Start ( ) {
        if( data == null )
            data = new DataHandler( );

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

        int servingValue = 1;
        try {
            servingValue = ( serving.text == "" ) ? 1 : Int32.Parse( this.serving.text );

        } catch( Exception e ) {
            Console.Write( e.ToString( )  + "\n\nServing text = " + serving.text );
            sendError( errorCode.NO_NUM_INPUT );
        }

        if( servingValue == 0 )
            sendError( errorCode.ZERO_INPUT );
        else if( servingValue < 0 )
            sendError( errorCode.NEGATIVE_INPUT );

        if( error )
            return;

        mealValue--;
        try {
            data.setCalories( meals[ mealValue ][ DataHandler.CALORIES_INDEX + 1 ], servingValue );
            data.setFat( meals[ mealValue ][ DataHandler.FAT_INDEX + 1 ], servingValue );
            data.setProtein( meals[ mealValue ][ DataHandler.PROTEIN_INDEX + 1 ], servingValue );
            data.setCarbs( meals[ mealValue ][ DataHandler.CARBS_INDEX + 1 ], servingValue );

        } catch (Exception e) {
            sendError( errorCode.NO_NUM_INPUT );
        }

        if( error )
            return;

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
        print( "size = " + data.myMeals.size( ) );
        print( "mmm = " + data.myMeals.print( ) );
        for( int i = 0; i < data.myMeals.size( ); i++ ) {
           meals_DD.options.Add( new Dropdown.OptionData( data.myMeals.get( i ).getName( ) ) );
        }
    }
}
