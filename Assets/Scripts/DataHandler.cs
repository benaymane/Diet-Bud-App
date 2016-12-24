using UnityEngine;
using System;
using System.IO;
using System.Collections.Generic;

public class DataHandler : MonoBehaviour {
    public static MealList myMeals;

    /*public string CALS_FILE_NAME = "calories.db", 
        MEALS_FILE_NAME = "meals.db";
        */
    public string CALS_FILE_NAME = Application.persistentDataPath + "/calories.db",
    MEALS_FILE_NAME = Application.persistentDataPath + "/meals.db";

    public const int CALORIES_ARRAY_SIZE = 4,
        CALORIES_INDEX = 0,
        FAT_INDEX = 1,
        PROTEIN_INDEX = 2,
        CARBS_INDEX = 3;

    string[ ] calories;
    string[ ][ ] meals = new string[10][];

    static int meals_size = 10;
    public int curr_meals_size = 0;

    StreamReader outFile;
    StreamWriter inFile;

    //Make the calories file if it doesn't already exist. If it does just read it.
    public DataHandler() {
        openToWrite( CALS_FILE_NAME );

        //If the file doesn't exist make it.
        if( new FileInfo( CALS_FILE_NAME ).Length == 0 ) {
            inFile.Write( "0\n0\n0\n0\n" );
            calories = new string[ CALORIES_ARRAY_SIZE ] { "0", "0", "0", "0" };
        }

        close( );

        //if we havn't read any calorie counts yet, read them.
        if( calories == null ) {
            calories = new string[ CALORIES_ARRAY_SIZE ];
            string line;

            openToRead( CALS_FILE_NAME );

            for( int i = 0; i < CALORIES_ARRAY_SIZE; i++ ) {
                line = outFile.ReadLine( );
                calories[ i ] = line;
            }

            close( );
        }

        //Check there is a meal file.

        openToWrite( MEALS_FILE_NAME );
        close( );

        if( myMeals == null ) {
            myMeals = new MealList( );
            readMeals( );
        }
    }
    
    //Getters functions
    public double getCalories( ) {
        return Convert.ToDouble( calories[ CALORIES_INDEX ] );
    }

    public double getFat( ) {
        return Convert.ToDouble( calories[ FAT_INDEX ] );
    }

    public double getProtein( ) {
        return Convert.ToDouble( calories[ PROTEIN_INDEX ] );
    }

    public double getCarbs( ) {
        return Convert.ToDouble( calories[ CARBS_INDEX ] );
    }

    //Setters functions
    public void setCalories( double newCalories, int serving ) {
            calories[ CALORIES_INDEX ] = ( getCalories( ) + newCalories * serving ).ToString( );
            updateCalories( );
    }

    public void setFat( double newFat, int serving ) {
        try { 
            calories[ FAT_INDEX ] = ( getFat( ) + newFat * serving ).ToString( );
            updateCalories( );
        } catch( Exception e ) {
            Console.Write( e.ToString( ) );
            throw;
        }
    }

    public void setProtein( double newProtein, int serving ) {
        try {
            calories[ PROTEIN_INDEX ] = ( getProtein( ) + newProtein * serving ).ToString( );
            updateCalories( );
        } catch( Exception e ) {
            Console.Write( e.ToString( ) );
            throw;
        }
    }

    public void setCarbs( double newCarbs, int serving ) {
        try {
            calories[ CARBS_INDEX ] = ( getCarbs( ) + newCarbs * serving ).ToString( );
            updateCalories( );
        } catch( Exception e ) {
            Console.Write( e.ToString( ) );
            throw;
        }
    }

    /*
     Rewrites and updates Calories file.
     */
    public void updateCalories( ) {
        File.WriteAllLines( CALS_FILE_NAME, calories );
    }

    /*
     Adds a new meal to the meal file
     */
    public static void addToDB( string filename, string meal ) {
        StreamWriter file = new StreamWriter( filename, true );
        file.WriteLine( meal );
        file.Close( );
    }
    
    public void addToDB( List<Meal> newMeals ) {
        openToWrite( MEALS_FILE_NAME );

        foreach( Meal meal in newMeals ) {
            myMeals.addMeal( meal );
            inFile.WriteLine( myMeals.getLastMeal( ).toString( ) );
        }

        close( );
    }

    public void addToDB( string mealName, double cals, double fat, double prot, double carb ) {
        myMeals.addMeal( new Meal( mealName, cals, fat, prot, carb ) );

        openToWrite( MEALS_FILE_NAME );
        inFile.WriteLine( myMeals.getLastMeal( ).toString( ) );
        close( );

    }


    public void resetCals( ) {
        calories = new string[CALORIES_ARRAY_SIZE] { "0", "0", "0", "0" };
        updateCalories( );
    }

    /*
     Read all the meals from meal file.
    
    public string[][] readAllMeals( ) {
        

        openToRead( MEALS_FILE_NAME );
        string line;

        //If the file has meals in it
        if( new FileInfo( MEALS_FILE_NAME ).Length != 0 ) {

            //read them all
            while( ( line = outFile.ReadLine( ) ) != null ) {
                resize( );
                meals[ curr_meals_size ] = line.Split( '-' );
                print( line + " and " + curr_meals_size );
                curr_meals_size++;
            }
        }
        close( );
        return meals;
    } */

    private void readMeals( ) {
        openToRead( MEALS_FILE_NAME );
        string line = "?";

        //If the file has meals in it
        if( new FileInfo( MEALS_FILE_NAME ).Length != 0 ) {

            //read them all
            while( ( line = outFile.ReadLine( ) ) != null ) {
                
                myMeals.addMeal( new Meal( line ) );
            }
            
        }
        
        close( );
    }

    public void removeMeal( int pos ) {
        myMeals.removeAt( pos );
        File.WriteAllLines( MEALS_FILE_NAME, myMeals.toStringArray( ) );
    }

    public void refreshMeals( ) {
        File.WriteAllLines( MEALS_FILE_NAME, myMeals.toStringArray( ) );
    }

    //Used to open a file for reading.
    void openToRead( string filename ) {
        outFile = new StreamReader( filename );
    }

    //User to open a file for writing.
    void openToWrite( string filename ) {
        inFile = new StreamWriter( filename, true );
    }

    //closes which ever file that's open
    void close( ) {
        if( inFile != null )
            inFile.Close( );

        if( outFile != null )
            outFile.Close( );
    }

}
