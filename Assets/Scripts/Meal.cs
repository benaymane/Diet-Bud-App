using System.Collections.Generic;

public class Meal {
    string mealName;
    double calories;
    double protein;
    double carbs;

    //const
    public Meal( string name, double cal, double prot, double carb ) {
        setName( name );
        setCalories( cal );
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

    public void setProtein( double protein ) {
        this.protein = protein;
    }

    public void setCarbs( double carbs ) {
        this.carbs = carbs;
    }

    public string toString( ) {
        return mealName+"-"+calories+"-"+protein+"-"+carbs;
    }
}

public static class MealList {
    static List<Meal> list;

    public static void addMeal( Meal meal ) {
        list.Add( meal );
    }

    public static void deleteMeal( int index ) {
        list.RemoveAt( index );
    }
}
