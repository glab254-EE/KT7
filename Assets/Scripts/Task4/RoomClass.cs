using UnityEngine;
using UnityEngine.UI;

public class RoomClass : MonoBehaviour {

    public int RoomNumber = 0;
    public string PersonInRoom;
    public bool HaveAnimal;
    [SerializeField] private TMPro.TMP_InputField RoomNumInput;
    [SerializeField] private TMPro.TMP_InputField PersonNameInput;
    [SerializeField] private UnityEngine.UI.Toggle ToggleAnimal;
    [SerializeField] private TMPro.TMP_Text RoomNAmeText;
    [SerializeField] private TMPro.TMP_Text RommNumberText;

    private void ChangeRoomNum(string text)
    {
        if (int.TryParse(text, out int newnum) == true && issetupcmplt)
        {
            RoomNumber = newnum;
            gameObject.name = text;
        }
        RommNumberText.text = $"{RoomNumber}";
    }
    private void OnPersonEdit(string text)
    {
        PersonInRoom = text;
    }
    private void OnAnimalChange(bool value)
    {
        HaveAnimal = value;
    }
    bool issetupcmplt;
    private void FixedUpdate()
    {
        if (issetupcmplt)
        {
            RommNumberText.text = $"{RoomNumber}";
            PersonNameInput.text = PersonInRoom;
            ToggleAnimal.isOn = HaveAnimal;
        }
    }
    private void Start()
    {
        RommNumberText.text = "0";
        PersonNameInput.text = PersonInRoom;
        RoomNumInput.onEndEdit.AddListener(ChangeRoomNum);
        PersonNameInput.onEndEdit.AddListener(OnPersonEdit);
        ToggleAnimal.onValueChanged.AddListener(OnAnimalChange);
    }
    public void SetUpR(int SettedRoomNum, string[] PeopleNames)
    {
        RoomNumber = SettedRoomNum;
        if (PeopleNames != null && PeopleNames.Length > 0) PersonInRoom = PeopleNames[UnityEngine.Random.Range(0, PeopleNames.Length)];
        HaveAnimal = UnityEngine.Random.Range(0, 10) > 6;
        ToggleAnimal.isOn = HaveAnimal;
        RoomNumInput.text = $"{SettedRoomNum}";
        issetupcmplt = true;
    }
}
