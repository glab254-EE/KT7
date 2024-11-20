using UnityEngine;
using UnityEngine.UI;

public class RoomClass : MonoBehaviour {

    [SerializeField] private int RoomNumber;
    [SerializeField] public string PersonInRoom { get; protected internal set; }
    [SerializeField] public bool HaveAnimal { get; protected internal set; }
    [SerializeField] private TMPro.TMP_InputField RoomNumInput;
    [SerializeField] private TMPro.TMP_InputField PersonNameInput;
    [SerializeField] private UnityEngine.UI.Toggle ToggleAnimal;
    int oldnum;
    private void ChangeRoomNum(string text)
    {
        if (int.TryParse(text, out int newnum) == true)
        {
            RoomNumber = newnum;
            oldnum = RoomNumber;
        }
        RoomNumInput.text = oldnum.ToString();
    }
    private void OnPersonEdit(string text)
    {
        PersonInRoom = text;
    }
    private void OnAnimalChange(bool value)
    {
        HaveAnimal = value;
    }
    private void Start()
    {
        RoomNumber = 0;
        oldnum = RoomNumber;
        RoomNumInput.text = oldnum.ToString();
        PersonNameInput.text = PersonInRoom;
        RoomNumInput.onEndEdit.AddListener(ChangeRoomNum);
        PersonNameInput.onEndEdit.AddListener(OnPersonEdit);
        ToggleAnimal.onValueChanged.AddListener(OnAnimalChange);
    }
    public void SetUpR(int SettedRoomNum, string[] PeopleNames)
    {
        if (SettedRoomNum > 0) RoomNumber = SettedRoomNum;
        if (PeopleNames != null && PeopleNames.Length > 0) PersonInRoom = PeopleNames[UnityEngine.Random.Range(0, PeopleNames.Length)];
        HaveAnimal = UnityEngine.Random.Range(0, 10) > 6;
        ToggleAnimal.isOn = HaveAnimal;
        oldnum = RoomNumber;
    }
}
