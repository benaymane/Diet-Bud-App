using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class MainDesign : MonoBehaviour {
    public GameObject backgrnd_B;
    public GameObject editPnl;

    public Sprite on, off;

    public Button soundBtn;

    public AudioSource music;

    public Dropdown MEAL_DD_B;

    public InputField mealName, cal, fat, prot, carb;
    // Use this for initialization
    void Start( ) {
        hideAll( );
        checkMusic( );
    }
    

    public void showManager( ) {
        backgrnd_B.SetActive( true );
    }

    public void hideAll( ) {
        backgrnd_B.SetActive( false );
        editPnl.SetActive( false );
    }

    public void showEditor( ) {

        int mealValue = MEAL_DD_B.value - 1;


        editPnl.SetActive( true );

        if( mealValue < 0 )
            return;

        Meal onMeal = DataHandler.myMeals.get( mealValue );

        mealName.text = onMeal.getName( );
        cal.text = onMeal.getCalories( ).ToString( );
        fat.text = onMeal.getFat( ).ToString( );
        prot.text = onMeal.getProtein( ).ToString( );
        carb.text = onMeal.getCarbs( ).ToString( );
               
    }

    void checkMusic( ) {

        if( GlobalVariables.soundOption )
            soundBtn.GetComponent<Image>( ).sprite = on;
        else
            soundBtn.GetComponent<Image>( ).sprite = off;

        music.mute = !GlobalVariables.soundOption;
    }

    public void mute( ) {

        GlobalVariables.soundOption = !GlobalVariables.soundOption;

        checkMusic( );

    }

}