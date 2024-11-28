using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class Task4_Main : MonoBehaviour
{
    [SerializeField] protected List<RoomClass> Rooms;
    [SerializeField] protected string[] PeopleNames = { "John", "Smith", "Oston", "Pablo", "Joshua", "Kevin", "Karen", "Elisabeth", "Alex" };
    [SerializeField] protected GameObject Prefab;

    [SerializeField] private string PickedName = "Smith";
    [SerializeField] private Button ResetBut;
    [SerializeField] private Button ActionBut;
    [SerializeField] private Transform Parent;
    private int CountThings(object val)
    {
        int outp = -1;
        if (val == null)
        {
            return -1;
        }
        if (val.GetType() == typeof(bool))
        {
            outp = 0;
            foreach (RoomClass room in Rooms)
            {
                if (room == null) continue;
                if (room.HaveAnimal == val.ConvertTo<bool>())
                {
                    outp++;
                }
            }
        }
        else if (val.GetType() == typeof(string))
        {
            outp = 0;
            foreach (RoomClass room in Rooms)
            {
                if (room == null) continue;
                if (room.PersonInRoom == val.ToString())
                {
                    outp++;
                }
            }
        } else if (double.TryParse(val.ToString(), out double x) == true)
        {
            outp = 0;
            double divido = Math.Pow(10, x);
            if (Rooms.Count >= divido)
            {
                outp = int.Parse(divido.ToString())-1; // to get value up until 10^x, not including it, f.e. 1,2,3...9  
            }
            else
            {
                outp = Rooms.Count;
            }
        }
        return outp;
    }
    private void ClearRooms()
    {
        foreach (Transform child in Parent)
        {
            if (child.gameObject.TryGetComponent<RoomClass>(out RoomClass roomClass) == true)
            {
                DestroyImmediate(child.gameObject);
            }
        }
    }
    private void AddRooms()
    {
        int max = UnityEngine.Random.Range(9, 16);
        for (int i = 0; i < max; i++)
        {
            GameObject newchild = Instantiate(Prefab, Parent);
            RoomClass roomClass = newchild.GetComponent<RoomClass>();
            roomClass.SetUpR(i+1, PeopleNames);
            newchild.name = i.ToString();
            Rooms.Add(roomClass);
        }
    }
    private void PrintRooms()
    {
        for (int i = 0;i < Rooms.Count; i++)
        {
            RoomClass room = Rooms[i];
            if (room != null && room.gameObject.activeInHierarchy == true)
            {
                string animalstring = room.HaveAnimal == true ? "With" : "Without";
                Debug.Log((i + 1).ToString() + $" - room number {room.RoomNumber}; Room Inhabitant: {room.PersonInRoom}, and {animalstring} an animal.");
            }
        }
    }
    private void ReverseOrder()
    {
        for (int i = 0; i < Rooms.Count/2; i++) // divided by two because after completing first half of rooms, second half is already will be done.
        {
            if (Rooms[i] == null || ((Rooms.Count / 2 )% 1 < 1 && i == Rooms.Count)) continue;
            RoomClass startroom = Rooms[i];
            int pick = Rooms.Count - i;
            RoomClass pickedroom = Rooms[Math.Clamp(pick,0,Rooms.Count-1)];
            bool havean = pickedroom.HaveAnimal;
            string oldname = pickedroom.PersonInRoom;
            pickedroom.HaveAnimal = startroom.HaveAnimal;
            pickedroom.PersonInRoom = startroom.PersonInRoom;
            startroom.HaveAnimal = havean;
            startroom.PersonInRoom = oldname;
        }
    }
    RoomClass[] oldrooms;
    private void OnRevertButtonPress()
    {
        if (oldrooms == null) return;
        for (int i = 0; i < oldrooms.Length; i++)
        {
            RoomClass currentroom = Rooms[Math.Clamp(i,0,Rooms.Count)];
            RoomClass oldcurrentroom = oldrooms[i];
            if (currentroom == null || oldcurrentroom == null) continue; // reseting starts
            currentroom.gameObject.SetActive(true);
            currentroom.HaveAnimal = oldcurrentroom.HaveAnimal; // converting starts
            currentroom.PersonInRoom = oldcurrentroom.PersonInRoom;
            currentroom.RoomNumber = oldcurrentroom.RoomNumber; // ends , reseting ends
        }
        PrintRooms();
    }
    private void OnPrimaryButtonClick()
    {
        oldrooms = new RoomClass[Rooms.Count];
        oldrooms = Rooms.ToArray();
        Debug.Log(CountThings(true).ToString()+" Animal Count");
        Debug.Log(CountThings(PickedName).ToString()+" Smith count");
        Debug.Log(CountThings(1).ToString()+ " Numbers under 10");
        ReverseOrder();
        PrintRooms();
        for (int i = 0; i < Rooms.Count; i++)
        {
            RoomClass room = Rooms[i];
            if (room.RoomNumber % 3 == 0 )
            {
                room.gameObject.SetActive(false);
            }
            else continue;
        }
        PrintRooms();
        RoomClass foundroom = null;
        int foundlocation = Rooms.Count;
        for (int i = Rooms.Count-1; i > 0; i--)
        {
            RoomClass room = Rooms[i];
            if (room.HaveAnimal && foundroom == null)
            {
                foundlocation = Rooms.Count-i;
                foundroom = room;
                break;
            }
        }
        string oldroomperson = "";
        for (int i = 0; i < Rooms.Count; i++)
        {
            RoomClass room = Rooms[i];
            if (!room.HaveAnimal)
            {
                oldroomperson = room.PersonInRoom;
                room.PersonInRoom = foundroom.PersonInRoom;
                room.HaveAnimal = true;
                foundroom.HaveAnimal = false;
                foundroom.PersonInRoom = oldroomperson;
                break;
            }
            else if (foundroom == null)
            {
                break;
            }
        }
        PrintRooms();
    }
    void Start()
    {
        ClearRooms();
        AddRooms();
        ActionBut.onClick.AddListener(OnPrimaryButtonClick);
        ResetBut.onClick.AddListener(OnRevertButtonPress);
    }

}
