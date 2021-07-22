/*******************************
 
DualShockボタン入力用（UnityInputだより） 

 *******************************/


using UnityEngine;

namespace DualShockInput
{
    public class DSInput
    {
        public static bool Push(DSButton ds) { return Input.GetKey((KeyCode)ds); }
        public static bool PushDown(DSButton ds) { return Input.GetKeyDown((KeyCode)ds); }
        public static bool PushUp(DSButton ds) { return Input.GetKeyUp((KeyCode)ds); }

    }

}
