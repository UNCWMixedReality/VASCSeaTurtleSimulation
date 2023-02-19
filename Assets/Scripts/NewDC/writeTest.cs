using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using UnityEngine;

public class writeTest : MonoBehaviour
{    
    public void write(string msg)
    {
        /*string newFileName = "C:\\client_20100913.csv";
        string clientDetails = clientNameTextBox.Text + "," + mIDTextBox.Text + "," + billToTextBox.Text;
        if (!File.Exists(newFileName))
        {
            string clientHeader = "Client Name(ie. Billto_desc)" + "," + "Mid_id,billing number(ie billto_id)" + "," + "business unit id" + Environment.NewLine;
            File.WriteAllText(newFileName, clientHeader);
        }
        File.AppendAllText(newFileName, clientDetails);
        */


        // Write each directory name to a file.
        using (StreamWriter sw = new StreamWriter(Application.persistentDataPath + "/test.csv", append: true))
        {
            sw.WriteLine(msg);
        }
    }
}
