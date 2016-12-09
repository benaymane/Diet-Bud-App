using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class ErrorHandler : MonoBehaviour {
    public GameObject status;

    protected float sec = 3;

    protected bool error = false;
    protected enum errorCode { SERVING_EMPTY, NAME_EMPTY, CAL_EMPTY,
        FAT_EMPTY, PROTEIN_EMPTY, CARBS_EMPTY, NAME_CONTAINS_BAR,
        INVALID_MEAL };

    void Update( ) {
        if( sec >= 3 )
            status.SetActive( false );
        else {
            sec += Time.deltaTime;
        }
    }

    void changeColor( Color color ) {
        status.GetComponent<Text>( ).color = color;
    }

    void changeText( string text ) {
        status.GetComponent<Text>( ).text = text;
    }

    public void sendGood( string msg ) {
        sec = 0;
        status.SetActive( true );
        changeColor( Color.green );
        changeText( msg );
    }

   protected void sendError( errorCode code ) {
        error = true;
        sec = 0;
        status.SetActive( true );
        changeColor( Color.red );

        switch( code ) {
            case errorCode.NAME_EMPTY :
                changeText( "The meal name is empty!" );
                break;

            case errorCode.NAME_CONTAINS_BAR :
                changeText( "The meal name should not contain a \"-\"" );
                break;

            case errorCode.CAL_EMPTY :
                changeText( "The meal calories field is empty!" );
                break;

            case errorCode.FAT_EMPTY:
                changeText( "The meal fat field is empty!" );
                break;

            case errorCode.PROTEIN_EMPTY:
                changeText( "The meal protein field is empty!" );
                break;

            case errorCode.CARBS_EMPTY:
                changeText( "The meal carbs field is empty!" );
                break;

            case errorCode.INVALID_MEAL:
                changeText( "Please choose a valid meal!" );
                break;
        }
    }


}
