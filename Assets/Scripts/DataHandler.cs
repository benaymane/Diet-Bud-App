using UnityEngine;
using System;
using System.IO;
using System.Collections;

public class DataHandler : MonoBehaviour {
    public const string CALS_FILE_NAME = "calories.db", 
        MEALS_FILE_NAME = "meals.db";

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
    }
    
    //Getters functions
    public int getCalories( ) {
        return Int32.Parse( calories[ CALORIES_INDEX ] );
    }

    public int getFat( ) {
        return Int32.Parse( calories[ FAT_INDEX ] );
    }

    public int getProtein( ) {
        return Int32.Parse( calories[ PROTEIN_INDEX ] );
    }

    public int getCarbs( ) {
        return Int32.Parse( calories[ CARBS_INDEX ] );
    }

    //Setters functions
    public void setCalories( string newCalories, int serving ) {
        
        calories[ CALORIES_INDEX ] = ( getCalories( ) + Int32.Parse( newCalories ) * serving ).ToString( );
        updateCalories( );
    }

    public void setFat( string newFat, int serving ) {
        calories[ FAT_INDEX ] = ( getFat( ) + Int32.Parse( newFat ) * serving ).ToString( );
        updateCalories( );
    }

    public void setProtein( string newProtein, int serving ) {
        calories[ PROTEIN_INDEX ] = ( getProtein( ) + Int32.Parse( newProtein ) * serving ).ToString( );
        updateCalories( );
    }

    public void setCarbs( string newCarbs, int serving ) {
        calories[ CARBS_INDEX ] = ( getCarbs( ) + Int32.Parse( newCarbs ) * serving ).ToString( );
        updateCalories( );
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
    
    public void resetCals( ) {
        calories = new string[CALORIES_ARRAY_SIZE] { "0", "0", "0", "0" };
        updateCalories( );
    }

    /*
     Read all the meals from meal file.
     */
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
    }

    void resize( ) {

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
