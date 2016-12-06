using UnityEngine;
using System;
using System.IO;
using System.Collections;

public class DataHandler : MonoBehaviour {
    const string DATA_FILE_NAME = "calories.db", 
        MEALS_FILE_NAME = "meals.db";

    const int CALORIES_ARRAY_SIZE = 4,
        CALORIES_INDEX = 0,
        FAT_INDEX = 1,
        PROTEIN_INDEX = 2,
        CARBS_INDEX = 3;

    int[ ] calories;
    string[ ][ ] meals = new string[10][];

    static int meals_size = 10;
    public static int curr_meals_size = 0;

    StreamReader outFile;
    StreamWriter inFile;

    //Make the calories file if it doesn't already exist. If it does just read it.
    public DataHandler() {
        openToWrite( DATA_FILE_NAME, true );

        //If the file doesn't exist make it.
        if( new FileInfo( DATA_FILE_NAME ).Length == 0 ) {
            inFile.Write( "0\n0\n0\n0\n" );
            calories = new int[ CALORIES_ARRAY_SIZE ] { 0, 0, 0, 0 };
        }

        close( );

        //if we havn't read any calorie counts yet, read them.
        if( calories == null ) {
            calories = new int[ CALORIES_ARRAY_SIZE ];
            string line;

            openToRead( DATA_FILE_NAME );

            for( int i = 0; i < CALORIES_ARRAY_SIZE; i++ ) {
                line = outFile.ReadLine( );
                calories[ i ] = Int32.Parse( line );
            }

            close( );
        }

        //Check there is a meal file.

        openToWrite( MEALS_FILE_NAME, true );
        close( );
    }
    
    //Getters functions
    public int getCalories( ) {
        return calories[ CALORIES_INDEX ];
    }

    public int getFat( ) {
        return calories[ FAT_INDEX ];
    }

    public int getProtein( ) {
        return calories[ PROTEIN_INDEX ];
    }

    public int getCarbs( ) {
        return calories[ CARBS_INDEX ];
    }

    //Setters functions
    public void setCalories( int newCalories ) {
        calories[ CARBS_INDEX ] = newCalories;
        updateCalories( );
    }

    public void setFat( int newFat ) {
        calories[ FAT_INDEX ] = newFat;
        updateCalories( );
    }

    public void setProtein( int newProtein ) {
        calories[ PROTEIN_INDEX ] = newProtein;
        updateCalories( );
    }

    public void setCarbs( int newCarbs ) {
        calories[ CARBS_INDEX ] = newCarbs;
        updateCalories( );
    }

    /*
     Rewrites and updates Calories file.
     */
    public void updateCalories( ) {
        openToWrite( DATA_FILE_NAME, false );

        for( int i = 0; i < CALORIES_ARRAY_SIZE; i++ )
            inFile.WriteLine( calories[ i ] );

        close( );
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
                meals[ curr_meals_size++ ] = line.Split( '\t' );
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
    void openToWrite( string filename, bool rewrite ) {
        inFile = new StreamWriter( filename, rewrite );
    }

    //closes which ever file that's open
    void close( ) {
        if( inFile != null )
            inFile.Close( );

        if( outFile != null )
            outFile.Close( );
    }

}
