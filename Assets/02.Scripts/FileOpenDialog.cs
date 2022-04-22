using UnityEngine;
using System.IO;
using System.Windows.Forms;
using Ookii.Dialogs;



public class FileOpenDialog : MonoBehaviour

{
    VistaOpenFileDialog OpenDialog;
    Stream openStream = null;

    private void Start()

    {

        OpenDialog = new VistaOpenFileDialog();
        OpenDialog.Filter = "jpg files (*.jpg) |*.jpg|png files (*.png) |*.jpg|All files  (*.*)|*.*";
        OpenDialog.FilterIndex = 3;
        OpenDialog.Title = "Image Dialog";

    }



    public string FileOpen()
    {

        if (OpenDialog.ShowDialog() == DialogResult.OK)
        {
            if ((openStream = OpenDialog.OpenFile()) != null)
            {
                return OpenDialog.FileName;
            }
        }
        return null;
    }



    public void OnGUI()
    {
        if (GUI.Button(new Rect(100, 100, 100, 50), "FileOpen"))
        {
            string fileName = FileOpen();

            if (!string.IsNullOrEmpty(fileName))
            {
                Debug.Log(fileName);
            }
        }
    }
}