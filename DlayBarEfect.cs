using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class DlayBarEfect: MonoBehaviour
{

    [Space(10)]
    [Header("UI Bar Settings")]
    [Space(5)]

    //UI Bar
    public Image FrontBar;
    public Image BackBar;

    //Bar Motion velocity
    public float VelocityForntBar, VelocityBackBar;

    float FrontVariable, BackVariable;

    bool InfoWasSeted = false;

    public void UpdateVariable(int PreviousVariable, int NewVariable, int MaxVariable)
    {

        #region InicialInformations

        if (!InfoWasSeted)
        {

            StartUIBar(NewVariable, MaxVariable);

            return;
        }

        #endregion

        #region CallCoroutines

        StopCoroutine("PreviousValuesEfectPositive");
        StopCoroutine("PreviousValuesEfectNegative");

        if (NewVariable - PreviousVariable >= 0)
        {

            object[] CoroutineValues = new object[2] { NewVariable, MaxVariable };

            StartCoroutine("PreviousValuesEfectPositive", CoroutineValues);

        }
        else
        {

            object[] CoroutineValues = new object[2] { NewVariable, MaxVariable };

            StartCoroutine("PreviousValuesEfectNegative", CoroutineValues);

        }

        #endregion

    }

    public void StartUIBar(int NewVariable, int MaxVariable)
    {

        FrontVariable = NewVariable;
        BackVariable = NewVariable;

        FrontBar.fillAmount = (float)FrontVariable / MaxVariable;
        BackBar.fillAmount = (float)BackVariable / MaxVariable;

    }

    #region PositiveEfect

    IEnumerator PreviousValuesEfectPositive(object[] CoroutineValues)
    {
 
        //SetValues
        int NewVariable = (int)CoroutineValues[0];
        int MaxVariable = (int)CoroutineValues[1];

        do
        {

            while (BackVariable < NewVariable)//first Moviment, BackBar
            {

                float BackVelocityFixed;

                if (BackVariable < NewVariable)
                {

                    BackVelocityFixed = (BackVariable + VelocityBackBar > NewVariable) ? NewVariable - BackVariable : VelocityBackBar;

                }
                else
                {

                    BackVelocityFixed = (BackVariable - VelocityBackBar < NewVariable) ? NewVariable - BackVariable : -VelocityBackBar;

                }

                BackVariable += BackVelocityFixed;

                BackBar.fillAmount = (float)BackVariable / MaxVariable;

                yield return new WaitForSeconds(0.02f);

            }
           
            //Second Moviment, FrontBar
            float FrontVelocityFixed;

            if (FrontVariable < NewVariable)
            {

                FrontVelocityFixed = (FrontVariable + VelocityForntBar > NewVariable) ? NewVariable - FrontVariable : VelocityForntBar;

            }
            else
            {

                FrontVelocityFixed = (FrontVariable - VelocityForntBar < NewVariable) ? NewVariable - FrontVariable : -VelocityForntBar;

            }

            FrontVariable += FrontVelocityFixed;

            FrontBar.fillAmount = (float)FrontVariable / MaxVariable;

            yield return new WaitForSeconds(0.02f);

        }
        while (FrontVariable < NewVariable);

    }
    
    #endregion

    #region PositiveEfect

    IEnumerator PreviousValuesEfectNegative(object[] CoroutineValues)
    {

        int NewVariable = (int)CoroutineValues[0];
        int MaxVariable = (int)CoroutineValues[1];

        do
        {

            while (FrontVariable != NewVariable)//first Moviment, FrontBar
            {

                float FrontVelocityFixed;

                if (FrontVariable < NewVariable)
                {

                    FrontVelocityFixed = (FrontVariable + VelocityForntBar > NewVariable) ? NewVariable - FrontVariable : VelocityForntBar;

                }
                else
                {

                    FrontVelocityFixed = (FrontVariable - VelocityForntBar < NewVariable) ? NewVariable - FrontVariable : -VelocityForntBar;

                }

                FrontVariable += FrontVelocityFixed;

                FrontBar.fillAmount = (float)FrontVariable / MaxVariable;

                yield return new WaitForSeconds(0.02f);

            }

            //Second Moviment, Back
            float BackVelocityFixed;

            if (BackVariable < NewVariable)
            {

                BackVelocityFixed = (BackVariable + VelocityBackBar > NewVariable) ? NewVariable - BackVariable : VelocityBackBar;

            }
            else
            {

                BackVelocityFixed = (BackVariable - VelocityBackBar < NewVariable) ? NewVariable - BackVariable : -VelocityBackBar;

            }

            BackVariable += BackVelocityFixed;

            BackBar.fillAmount = (float)BackVariable / MaxVariable;

            yield return new WaitForSeconds(0.02f);

        }
        while (BackVariable != NewVariable);

    }

    #endregion

}
