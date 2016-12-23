using UnityEngine;
using System;
using UnityEngine.UI;
using System.Collections.Generic;

public class MainScript : ErrorHandler {

    public static List<Meal> newMeals;

    public DataHandler data;

    public Text caloriesValue,
        fatValue,
        proteinValue,
        carbsValue;

    public Dropdown meals_DD;
    public Dropdown meals_DD_B;

    public InputField serving;

    public string[][] meals;

	// Use this for initialization
	void Start ( ) {

        if( data == null )
            data = new DataHandler( );


        if( newMeals == null )
            newMeals = new List<Meal>( );

        if( newMeals.Count != 0 ) {
            data.addToDB( newMeals );
            newMeals = new List<Meal>( );
        }

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
        data.setCalories( DataHandler.myMeals.get( mealValue ).getCalories( ), servingValue );
        data.setFat( DataHandler.myMeals.get( mealValue ).getFat( ), servingValue );
        data.setProtein( DataHandler.myMeals.get( mealValue ).getProtein( ), servingValue );
        data.setCarbs( DataHandler.myMeals.get( mealValue ).getCarbs( ), servingValue );

        init_cals( );

        sendGood( DataHandler.myMeals.get( mealValue ).getName( ) + " has been counted in! " );
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
        print( "size = " + DataHandler.myMeals.size( ) );
        print( "mmm = " + DataHandler.myMeals.print( ) );

        for( int i = 0; i < DataHandler.myMeals.size( ); i++ ) {
           meals_DD.options.Add( new Dropdown.OptionData( DataHandler.myMeals.get( i ).getName( ) ) );
            meals_DD_B.options.Add( new Dropdown.OptionData( DataHandler.myMeals.get( i ).getName( ) ) );
        }
    }
}
