using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


public class Milestone
{
    public string Name;
    public IEnumerator enumerator;

    public Milestone(string name, IEnumerator enumerator)
    {
        this.Name = name;
        this.enumerator = enumerator;
    }
}
