using System.Collections.Generic;
using System;

public class Meal {
    //consts
    static int NAME_INDEX = 0,
        CALORIES_INDEX = 1,
        FAT_INDEX = 2,
        PROTEIN_INDEX = 3,
        CARBS_INDEX = 4;

    string mealName;
    double calories;
    double fat;
    double protein;
    double carbs;

    //const
    public Meal( ) {

    }

    public Meal( string fullMeal ) {
        if( fullMeal == null ) {
            Console.Write( "full meal is null " );
            return;
        }

        string[] choppedMeal = fullMeal.Split( '-' );

        setName( choppedMeal[NAME_INDEX] );
        setCalories( Double.Parse( choppedMeal[ CALORIES_INDEX ] ) );
        setFat( Double.Parse( choppedMeal[ FAT_INDEX ] ) );
        setProtein( Double.Parse( choppedMeal[ PROTEIN_INDEX ] ) );
        setCarbs( Double.Parse( choppedMeal[ CARBS_INDEX ] ) );
    }

    public Meal( string name, string cal, string fat, string prot, string carb ) {
        setName( name );
        setCalories( Double.Parse( cal ) );
        setFat( Double.Parse( fat ) );
        setProtein( Double.Parse( prot ) );
        setCarbs( Double.Parse( carb ) );
    }

    public Meal( string name, double cal, double fat, double prot, double carb ) {
        setName( name );
        setCalories( cal );
        setFat( fat );
        setProtein( prot );
        setCarbs( carb );
    }

    //getters
    public string getName( ) {
        return mealName;
    }

    public double getCalories( ) {
        return calories;
    }

    public double getFat( ) {
        return fat;
    }

    public double getProtein( ) {
        return protein;
    }

    public double getCarbs( ) {
        return carbs;
    }

    //setters
    public void setName( string name ) {
        mealName = name;
    }

    public void setCalories( double calories ) {
        this.calories = calories;
    }

    public void setFat( double fat ) {
        this.fat = fat;
    }

    public void setProtein( double protein ) {
        this.protein = protein;
    }

    public void setCarbs( double carbs ) {
        this.carbs = carbs;
    }

    public string toString( ) {
        return mealName+"-"+calories+"-"+fat+"-"+protein+"-"+carbs;
    }
    
}

public class MealList {
    List<Meal> list = new List<Meal>();

    public void addMeal( Meal meal ) {
        Console.WriteLine( "???????" );
        if( meal == null ) {
            Console.WriteLine( "meal is null?" );
            return;
        }
        list.Add( meal );
    }

    public Meal get(int index ) {
        return list[ index ];
    }

    public Meal getLastMeal( ) {
        return list[ list.Count - 1 ];
    }

    public void deleteMeal( int index ) {
        list.RemoveAt( index );
    }

    public int size( ) {
        return list.Count;
    }

    public string print( ) {
        return list[ 0 ].toString();
    }

    public void removeAt( int pos ) {
        list.RemoveAt( pos );
    }

    public void replace( int pos, Meal meal ) {
        list[ pos ] = meal;
    }

    public string[] toStringArray( ) {
        string[ ] arr = new string[ list.Count ];

        for( int i = 0; i < arr.Length; i++)
            arr[ i ] = list[ i ].toString( );

        return arr;
    }
}
