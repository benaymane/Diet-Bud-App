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

    public int[ ] calories;
    public string[ ][ ] meals;

    static int meals_size = 10;
    static int curr_meals_size = 0;

    StreamReader outFile;
    StreamWriter inFile;

    public DataHandler() {
        openToWrite( DATA_FILE_NAME, false );

        if( new FileInfo( DATA_FILE_NAME ).Length == 0 ) {
            inFile.Write( "0\n0\n0\n0\n" );
            calories = new int[ CALORIES_ARRAY_SIZE ] { 0, 0, 0, 0 };
        }

        close( );

        if( calories != null ) {
            calories = new int[ CALORIES_ARRAY_SIZE ];
            string line;

            openToRead( DATA_FILE_NAME );

            for( int i = 0; i < CALORIES_ARRAY_SIZE; i++ ) {
                line = outFile.ReadLine( );
                calories[ i ] = Int32.Parse( line );
            }

            close( );
        }
    }

	// Use this for initialization
	void Start ( ) {

	
	}
	
	// Update is called once per frame
	void Update ( ) {
	
	}

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

    public void updateCalories( ) {
        openToWrite( DATA_FILE_NAME, false );

        for( int i = 0; i < CALORIES_ARRAY_SIZE; i++ )
            inFile.WriteLine( calories[ i ] );

        close( );
    }

    public void readAllMeals( ) {
        openToRead( MEALS_FILE_NAME );
        string line;

        if( new FileInfo( MEALS_FILE_NAME ).Length != 0 ) {
            while( ( line = outFile.ReadLine( ) ) != null ) {
                //resize( );
                meals[ curr_meals_size++ ] = line.Split( '\t' );
            }
        }
    }

    void openToRead( string filename ) {
        outFile = new StreamReader( filename );
    }

    void openToWrite( string filename, bool rewrite ) {
        inFile = new StreamWriter( filename, rewrite );
    }

    void close( ) {
        if( inFile != null )
            inFile.Close( );

        if( outFile != null )
            outFile.Close( );
    }

}
