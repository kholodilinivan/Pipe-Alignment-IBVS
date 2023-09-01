using System;
using System.Net.Sockets;
using System.Text;
using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;
using System.Net;
using System.IO;
using UnityEngine.UI;
using Assets.ImageSynthesis;

public class Server_new : MonoBehaviour {

    //General Init
    private List<ServerClient> clients;
    private List<int> disconnectIndex;

    [Header("Server Settings")]
    public int Port;
    private TcpListener server;
    private bool serverStarted;
    private ServerUI_new UserInterface;

    [Header("Game Object Settings")]
    public string ObjectName = "drone";
    public string ObjectNameRobot = "robot";
    public string CameraName = "cam";
    public Vector2 Resolution = new Vector2(100,100);
    private int c;
    public GameObject robot;
    public InputField SetSpeed, SetRotSpeed;
    //  private byte gg = new byte[2];

    // Use this for initialization
    void Start () {
        clients = new List<ServerClient>();
        disconnectIndex = new List<int>();
        UserInterface = gameObject.GetComponent<ServerUI_new>();

        try {
            server = new TcpListener(IPAddress.Any, Port);
            server.Start();

            Startlistening();
            serverStarted = true;
            Debug.Log("Server has started on port:" + Port.ToString());
        }
        catch (Exception e) {
            Debug.Log("Socket Error " + e.Message);
        }

        InvokeRepeating("UpdateLoop", 0f, 0.003f);
    }

    private void UpdateLoop()
    {
        if(!serverStarted)
            return;
        if(clients.Count == 0)
            return;

        for(c = 0; c < clients.Count; c++ ) {
            //Check if clients are connected
            if(!isConnected(clients[c].tcp)) {
                clients[c].tcp.Close();
                disconnectIndex.Add(c);
                Debug.Log(clients[c].clientName + " has disconnected from the server");
                continue;
            }
            // Check for data from client
            else {
                float[] myFloat = new float[7];
                NetworkStream s = clients[c].tcp.GetStream();
                if (s.DataAvailable) {
                    byte[] RecievedString = new byte[sizeof(float)*7];

                    if (RecievedString != null)
                    {
                        s.Read(RecievedString, 0, sizeof(float)*7);
                        myFloat = ConvertBytes2Float(RecievedString);

                        int a = Convert.ToInt32(myFloat[0]);
                        if (a == 0)
                        {
                            Resolution.x = myFloat[1];
                            Resolution.y = myFloat[2];
                            StartCoroutine(SendCamCapture(clients[c], ObjectName, CameraName, Resolution.x.ToString(), Resolution.y.ToString()));
                        }
                        else if (a == 1)
                        {
                            //  ModifyGameObj(ObjectName, "TW", myFloat[1], myFloat[2], myFloat[3]);
                            //  ModifyGameObj(ObjectName, "RW", myFloat[4], myFloat[5], myFloat[6]);
                            RobotControl(myFloat[1], myFloat[2], myFloat[3], myFloat[4], myFloat[5], myFloat[6]);

                            //  gg = 10;
                        }
                        else if (a == 2)
                        {
                            GameObject getmotion = GameObject.Find("walle");
                            Changes getmotion_ = getmotion.GetComponent<Changes>();
                            float xx = float.Parse(getmotion_.robot_pos_x.text);
                            float yy = float.Parse(getmotion_.robot_pos_y.text);
                            float rot = float.Parse(getmotion_.robot_rot.text);

                            float[] ff = new float[]
                            {xx,yy,rot,0, 0,0,0,0, 0,0,0,0};

                            OutgoingDataFloat(clients[c], ff);
                            // ModifyGameObj(ObjectName, "TW", myFloat[1], myFloat[2], myFloat[3]);
                            // ModifyGameObj(ObjectName, "RW", myFloat[4], myFloat[5], myFloat[6]);
                            // RobotControl(myFloat[1], myFloat[2], myFloat[3], myFloat[4], myFloat[5], myFloat[6]);
                            // StartCoroutine(SendCamCapture(clients[c], ObjectName, CameraName, Resolution.x.ToString(), Resolution.y.ToString()));
                        }
                        else if (a == 3)
                        {
                            if (myFloat[1] == 0)
                            {
                                var effectType = (CameraEffectType)0;
                                Scripts_1.CameraEffectSettings.Instance.CurrentCameraEffect = effectType;
                            }
                            else if (myFloat[1] == 1)
                            {
                                var effectType = (CameraEffectType)2;
                                Scripts_1.CameraEffectSettings.Instance.CurrentCameraEffect = effectType;
                            }
                            //  RobotControl(myFloat[1], myFloat[2], myFloat[3], myFloat[4], myFloat[5], myFloat[6]);
                            //  Resolution.x = 320;
                            //  Resolution.y = 320;
                            //  StartCoroutine(SendCamCapture(clients[c], ObjectName, CameraName, Resolution.x.ToString(), Resolution.y.ToString()));
                            //  byte ww = Convert.ToByte(myFloat[1]);
                            //  byte ss = Convert.ToByte(myFloat[2]);
                            //  byte aa = Convert.ToByte(myFloat[3]);
                            //  byte dd = Convert.ToByte(myFloat[4]);
                            //  RobotControl(ww,ss,aa,dd);
                            //  RobotControl(myFloat[1], myFloat[2], myFloat[3], myFloat[4], myFloat[5], myFloat[6]);
                            //  StartCoroutine(SendCamCapture(clients[c], ObjectName, CameraName, Resolution.x.ToString(), Resolution.y.ToString()));
                        }
                        else if (a == 4)
                        {
                            GameObject laseractive = GameObject.Find("walle");
                            Changes laseractive_ = laseractive.GetComponent<Changes>();
                            laseractive_.LaserToogle(Convert.ToBoolean(myFloat[1]));
                        }
                        else if (a == 5)
                        {
                            GameObject setposition = GameObject.Find("walle");
                            Changes setposition_ = setposition.GetComponent<Changes>();
                            // setposition_.LaserToogle(Convert.ToBoolean(myFloat[1]));
                            float x = myFloat[1];
                            float y = myFloat[2];
                            float rot = myFloat[3];
                            setposition_.Robot.transform.localPosition = new Vector3(y / 1000, 0, x / 1000);
                            // setposition_.Robot.transform.localRotation = Quaternion.Euler(0, rot, 0); // for the walle robot
                            setposition_.Robot.transform.localRotation = Quaternion.Euler(rot, 0, 0);
                            // setposition_.b = 0;
                            // setposition_.a = 0;
                        }
                        else if (a == 6)
                        {
                            GameObject MotionContr = GameObject.Find("walle");

                            float speed = myFloat[1];
                            MotionContr.transform.position += MotionContr.transform.up * speed * Time.deltaTime;
                        }
                        else if (a == 7)
                        {
                            GameObject MotionContr = GameObject.Find("walle");

                            float speed = myFloat[1];
                            MotionContr.transform.position += MotionContr.transform.right * speed * Time.deltaTime;
                        }

                    }
                    s.Flush();
                    
                }
                
            }
        
        }

        //Clean up Disconnected Clients
        for(int i = 0; i < disconnectIndex.Count; i++) {
            clients.RemoveAt(disconnectIndex[i]);
        }
        disconnectIndex.Clear();
    }

    private byte[] ConvertFloat2Bytes(float[] FloatArray)
    {
        var byteArray = new byte[FloatArray.Length * sizeof(float)];
        Buffer.BlockCopy(FloatArray, 0, byteArray, 0, byteArray.Length);
        return byteArray;
    }

    private float[] ConvertBytes2Float(byte[] byteArray)
    {
        var floatArray = new float[byteArray.Length / sizeof(float)];
        Buffer.BlockCopy(byteArray, 0, floatArray, 0, byteArray.Length);
        return floatArray;
    }

    private void SendOutData(ServerClient client, int NumBytes)
    {
        bool flip = false;
        byte[] Tmp = new byte[NumBytes];
        for (int i = 0; i < NumBytes; i++)
        {
            if (i % 300 == 0)
                flip = !flip;

            if (flip)
                Tmp[i] = 255;
            else
                Tmp[i] = 0;
        }
        OutgoingData(client, Tmp);
    }

    //Checks if client is connected
    private bool isConnected(TcpClient c)
    {
        try {
            if(c != null && c.Client != null && c.Client.Connected) //Makes sure the client is connected
            {
                if(c.Client.Poll(0, SelectMode.SelectRead))         //Polls the Client for activity
                {
                    return !(c.Client.Receive(new byte[1], SocketFlags.Peek) == 0); //Checks for response
                }
                
                return true;
            }
            else
                return false;
        }
        catch {
            return false;
        }
    }
    //Begins connection with client
    private void AcceptServerClient(IAsyncResult ar)
    {
        TcpListener listener = (TcpListener)ar.AsyncState;
        ServerClient NewClient = new ServerClient(listener.EndAcceptTcpClient(ar), null);
        Debug.Log("Someone has connected");
        clients.Add(NewClient);
        Startlistening();
        //UserInterface.SetStatus("Connected");
    }

    //Starts listening on server socket
    private void Startlistening()
    {
        server.BeginAcceptTcpClient(AcceptServerClient, server);
    }

    //Try to close all the connections gracefully
    public void OnApplicationQuit()
    {
        for (int i = 0; i < clients.Count; i++)
        {
            try
            {
                clients[i].tcp.GetStream().Close();
                clients[i].tcp.Close();
                server.Stop();
            }
            catch { }

        }
        Debug.Log("Connections Closed");
    }

    //Sends out data
    public void OutgoingData(ServerClient c, byte[] data) {
        NetworkStream ClientStream = c.tcp.GetStream();
        try
        {
        ClientStream.Write(data, 0, data.Length);
        }
        catch (Exception e) {
            Debug.LogError("Could not write to client.\n Error:" + e);
        }
    }

    //Sends out data
    public void OutgoingDataFloat(ServerClient c, float[] data)
    {
        NetworkStream ClientStream = c.tcp.GetStream();
        try
        {
            ClientStream.Write(ConvertFloat2Bytes(data), 0, data.Length);
        }
        catch (Exception e)
        {
            Debug.LogError("Could not write to client.\n Error:" + e);
        }
    }

    //Translates or Rotates the GameObject according to the manipulaton type and translations with respect to world
    private void ModifyGameObj(string GameObjName,string ManipulationType, float x_trans, float y_trans, float z_trans)
    {

        GameObject ClientObj = GameObject.Find(GameObjName);
        Vector3 MatlabTransform = new Vector3(x_trans, y_trans, z_trans);
        switch (ManipulationType)
        {
            case "TW": //Translate relative to world
                ClientObj.transform.position = transform.TransformVector(MatlabTransform);
                break;
            case "RW": //Rotate relative to world
                ClientObj.transform.eulerAngles = transform.TransformVector(MatlabTransform);
                break;
            default:
                break;  //Just falls through
        }
        GameObject reset_ab = GameObject.Find("walle");
        Changes reset_ab_void = reset_ab.GetComponent<Changes>();
        reset_ab_void.Reset_syst();
    }

    //  private void RobotControl(byte W, byte S, byte A, byte D)
    private void RobotControl(float W, float S, float A, float D, float speed, float rotSpeed)
    {
        GameObject reset_ab = GameObject.Find("walle");
        Changes reset_ab_void = reset_ab.GetComponent<Changes>();
        walle stop_motion = reset_ab.GetComponent<walle>();
        reset_ab_void.Reset_syst();
        reset_ab_void.d = 1;
        stop_motion.d = 1;

        SetSpeed.text = Convert.ToString(speed);
        SetRotSpeed.text = Convert.ToString(rotSpeed);
        //  speed = int.Parse(SetSpeed.text);
        //  rotSpeed = int.Parse(SetRotSpeed.text);
        if (W==1)
        {
           // ClientObj.transform.position += ClientObj.transform.forward * speed * Time.deltaTime;
            robot.transform.position += robot.transform.forward * speed * Time.deltaTime;
        }
            if (S==1)
            {
                robot.transform.position -= robot.transform.forward * speed * Time.deltaTime;
                if (D == 1)
                {
                    robot.transform.Rotate(Vector3.down * rotSpeed);
                }
                if (A == 1)
                {
                    robot.transform.Rotate(Vector3.up * rotSpeed);
                }
            }
            else if (A==1)
            {
                robot.transform.Rotate(Vector3.down * rotSpeed);
            }
            else if (D==1)
            {
                robot.transform.Rotate(Vector3.up * rotSpeed);
            }
    }


    //Organizes and Sends Picture
    IEnumerator SendCamCapture(ServerClient c, string GameObjName, string CameraSelect, string Width, string Height)
    {
        GameObject ClientObj = GameObject.Find(GameObjName);
        QuadCopter_new QuadcopterComp = ClientObj.GetComponent<QuadCopter_new>();

        if (QuadcopterComp != null)
        {
            QuadcopterComp.CaptureImage(CameraSelect, int.Parse(Width), int.Parse(Height));
            while (!QuadcopterComp.CaptureDone())
            {
                yield return null;
            }
            
            OutgoingData(c, QuadcopterComp.ReturnCaptureBytes());
            Debug.Log("Captured Image");
        }
        else
        {
            Debug.LogError("Quadcopter Script not attached to " + GameObjName + "!!!");
        }
    }

    IEnumerator SerializeCapture(ServerClient c, byte[] PixelData, int Width, int Length)
    {
        OutgoingData(c, PixelData);
        yield return null;
    }


}

public class ServerClient {

    public TcpClient tcp;
    public string clientName;
    public List<GameObject> ClientObj;

    public ServerClient(TcpClient clientSocket, string Name) {
        clientName = Name;
        tcp = clientSocket;
        ClientObj = new List<GameObject>();
    }

}