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

    public GameObject stA, stB;

    public Dropdown meals_DD;
    public Dropdown meals_DD_B;

    public InputField serving,
        mealName, cal, fat, prot, carb;

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
        meals_DD.ClearOptions( );
        meals_DD_B.ClearOptions( );

        meals_DD.options.Add( new Dropdown.OptionData( "MEALS" ) );
        meals_DD_B.options.Add( new Dropdown.OptionData( "PICK A MEAL" ) );

        meals_DD.value = 0;
        meals_DD_B.value = 0;

        meals_DD.captionText.text = "MEALS";
        meals_DD_B.captionText.text = "PICK A MEAL";

        for( int i = 0; i < DataHandler.myMeals.size( ); i++ ) {
           meals_DD.options.Add( new Dropdown.OptionData( DataHandler.myMeals.get( i ).getName( ) ) );
            meals_DD_B.options.Add( new Dropdown.OptionData( DataHandler.myMeals.get( i ).getName( ) ) );
        }
    }

    public void resetST() {
        status = stA;
    }

    public void changeST( ) {
        status = stB;
    }
    

    public void deleteMeal( ) {
        error = false;

        int mealValue = meals_DD_B.value - 1;

        if( mealValue == -1 )
            sendError( errorCode.INVALID_MEAL );

        if( error )
            return;

        string name = DataHandler.myMeals.get( mealValue ).getName( );
        data.removeMeal( mealValue );
        init_meals( );

        sendGood( name + " is GONE!" );

        clearEditor( );
    }

    public void update( ) {
        error = false;

        if( mealName.text == "" )
            sendError( errorCode.NAME_EMPTY );

        else if( mealName.text.Contains( "-" ) )
            sendError( errorCode.NAME_CONTAINS_BAR );

        else if( cal.text == "" )
            sendError( errorCode.CAL_EMPTY );

        else if( fat.text == "" )
            sendError( errorCode.FAT_EMPTY );

        else if( prot.text == "" )
            sendError( errorCode.PROTEIN_EMPTY );

        else if( carb.text == "" )
            sendError( errorCode.CARBS_EMPTY );

        else if( !isNumeric( ) )
            sendError( errorCode.NO_NUM_INPUT );

        else if( !isPositive( ) )
            sendError( errorCode.NEGATIVE_INPUT );

        if( error )
            return;

        //This might need modification later, if someone change meals while this changes?
        int pos = meals_DD_B.value - 1;

        DataHandler.myMeals.replace( pos, new Meal( mealName.text, cal.text, fat.text, prot.text, carb.text ) );
        data.refreshMeals( );

        sendGood( mealName.text + " updated!" );

        clearEditor( );

    }

    void clearEditor( ) {
        mealName.text = cal.text = fat.text = prot.text = carb.text = "";
        meals_DD_B.value = 0;
    }

    bool isNumeric( ) {
        double flush;

        return ( Double.TryParse( cal.text, out flush ) && Double.TryParse( fat.text, out flush )
            && Double.TryParse( prot.text, out flush ) && Double.TryParse( carb.text, out flush ) );
    }

    bool isPositive( ) {
        return !( cal.text.Contains( "-" ) || fat.text.Contains( "-" ) ||
            prot.text.Contains( "-" ) || carb.text.Contains( "-" ) );
    }
}
