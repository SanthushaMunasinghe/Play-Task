using System.Collections;
using System.Collections.Generic;
using Newtonsoft.Json.Linq;
using UnityEngine;
using UnityEngine.UIElements;

public class Classroom : TeacherDashboardClassroom
{
    private int classIndex = 0;

    [SerializeField] private Color highlightColor;

    //UI Elements
    private Label classroomLabel;
    private ScrollView studentList;
    private List<VisualElement> studentItems = new List<VisualElement>();

    void Start()
    {
        classroomLabel = classroomBox.Q<VisualElement>("classroom-label").Q<Label>();

        var nextBtn = classroomBox.Q<Button>("classroom-next-btn");
        var backBtn = classroomBox.Q<Button>("classroom-back-btn");

        studentList = classroomBox.Q<ScrollView>("student-list");

        foreach (string id in GlobalData.classrooms)
        {
            GetClassData(id);
        }

        nextBtn.clicked += () =>
        {
            GlobalMethods.NextBackBtn(true, ref classIndex, classroomList.Count);
            SelectClassroomStudents();
        };

        backBtn.clicked += () =>
        {
            GlobalMethods.NextBackBtn(false, ref classIndex, classroomList.Count);
            SelectClassroomStudents();
        };
    }

    void Update()
    {
        if (classroomList.Count != 0)
        {
            Dictionary<string, string> currentClasssroom = classroomList[classIndex];
            classroomLabel.text = $"Classroom {currentClasssroom["Grade"]} - {currentClasssroom["Classroom"]}";
        }
    }

    private void SelectClassroomStudents()
    {
        studentList.Clear();

        if (studentDataList.Count != 0)
        {
            for (int i = 0; i < studentDataList.Count; i++)
            {
                if (studentDataList[i].Classroom == classroomList[classIndex]["Id"])
                {
                    DisplayStudentList(studentList, studentDataList[i], i + 1);
                }
            }
        }
    }

    private void GetClassData(string id)
    {
        // Define headers for the classroom request
        Dictionary<string, string> headers = new Dictionary<string, string>();
        headers.Add("Authorization", "Bearer <token>");

        classroomLabel.text = "Please Wait...";

        string idResponse;
        string nameResponse;
        string gradeResponse;

        sendRequests.SendGetRequest(GlobalData.url + "/getclassroombyid/" + id, headers, classroomLabel, (responseJson) => {

            //Get Classroom Name
            idResponse = responseJson["_id"].Value<string>();
            nameResponse = responseJson["name"].Value<string>();

            sendRequests.SendGetRequest(GlobalData.url + "/getgradebyid/" + responseJson["grade"], headers, classroomLabel, (responseJson) => {
                gradeResponse = responseJson["number"].Value<string>();

                Dictionary<string, string> currentClassroom = new Dictionary<string, string>();
                currentClassroom.Add("Id", idResponse);
                currentClassroom.Add("Classroom", nameResponse);
                currentClassroom.Add("Grade", gradeResponse);
                classroomList.Add(currentClassroom);

                if (classroomList.Count != 0)
                {
                    Dictionary<string, string> currentClasssroom = classroomList[0];
                    classroomLabel.text = $"Classroom {currentClasssroom["Grade"]} - {currentClasssroom["Classroom"]}";
                }
                else
                {
                    classroomLabel.text = "No Classes";
                }

                GetStudents(id);
            });

        });
    }

    private void GetStudents(string classroomId)
    {
        // Define headers for the classroom request
        Dictionary<string, string> headers = new Dictionary<string, string>();
        headers.Add("Authorization", "Bearer <token>");

        Label label = new Label();

        sendRequests.GetArray(GlobalData.url + "/getsclassroomtudents/" + classroomId, headers, label, (responseArray) => {

            if (responseArray.Count != 0)
            {
                foreach (JObject studentObject in responseArray)
                {
                    //Debug.Log(studentObject);
                    StudentData student = new StudentData();
                    student.Stdname = studentObject["name"].Value<string>();
                    student.StdID = studentObject["_id"].Value<string>();
                    student.Phone = studentObject["contactno"].Value<string>();
                    student.Email = studentObject["email"].Value<string>();
                    student.Home = studentObject["home"].Value<string>();
                    student.Classroom = studentObject["classroom"].Value<string>();
                    student.Subjects = studentObject["subjects"].ToObject<string[]>();
                    studentDataList.Add(student);
                }

                SelectClassroomStudents();
            }
        });
    }

    private void DisplayStudentList(ScrollView list, StudentData studentData, int number)
    {
        VisualElement newItem = new VisualElement();

        newItem.AddToClassList("list-primary-item");

        Label studentNo = new Label();
        Label studentName = new Label();
        Label studentEmail = new Label();

        studentNo.AddToClassList("list-primary-item-text");

        studentName.AddToClassList("list-primary-item-text");
        studentEmail.AddToClassList("list-primary-item-text");

        studentName.name = "studentName";

        studentNo.text = number.ToString() + ".";
        studentName.text = studentData.Stdname;
        studentEmail.text = studentData.Email;

        newItem.Add(studentNo);
        newItem.Add(studentName);
        newItem.Add(studentEmail);

        list.Add(newItem);

        newItem.RegisterCallback<MouseUpEvent>(evt => {
            foreach (StudentData std in studentDataList)
            {
                if (std.Stdname == newItem.Q<Label>("studentName").text)
                {
                    GetComponent<Student>().selectedStudent = std;

                    foreach (string subject in std.Subjects)
                    {
                        GetComponent<Student>().GetStudentSubjects(subject);
                    }
                }
            }
        });
    }
}
