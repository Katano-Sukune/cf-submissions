using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Text;
using System.Text.RegularExpressions;
using System.Linq;


public class AtCoder {
    static  int n = int.Parse(Console.ReadLine());
    static string[] l;
    static void Main() {
        l = new string[n];
        for(int i = 0; i < n; i++) {
            l[i] = Console.ReadLine();
        }
        bool ans = false;
      for(int i = 0; i < n; i++) {
           
                string[] a = l[i].Split('|');
                if(a[0]== "OO") {
                    l[i] = "++|" + a[1];
                ans = true;
                break;
            }
                else if(a[1]== "OO") {
                    l[i] = a[0] + "|++";
                ans = true;
                break;
            }
               
            
        }
        if (ans) {
            Console.WriteLine("YES");
            for(int i = 0; i < n; i++) {
                Console.WriteLine(l[i]);
            }
        }
        else {
            Console.WriteLine("NO");
            
        }

    }
}


