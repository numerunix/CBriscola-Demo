/*
  *  This code is distribuited under GPL 3.0 or, at your opinion, any later version
 *  CBriscola 1.1.3
 *
 *  Created by Giulio Sorrentino (numerone) on 29/01/23.
 *  Copyright 2023 Some rights reserved.
 *
 */


using Avalonia.Controls;
using Avalonia.Media.Imaging;
using Avalonia.Platform;
using CBriscola_Demo;
using System;
using System.IO;
using System.Reflection;
using System.Resources;

namespace org.altervista.numerone.framework
{
    public class Carta
    {
        private readonly UInt16 seme,
                   valore,
                   punteggio;
        private string semeStr;
        private readonly CartaHelperBriscola helper;
        private readonly static Carta[] carte = new Carta[40];
        private Bitmap img;

        private Carta(UInt16 n, CartaHelperBriscola h)
        {
            helper = h;
            seme = helper.GetSeme(n);
            valore = helper.GetValore(n);
            punteggio = helper.GetPunteggio(n);
        }
        public static void Inizializza(Mazzo m, ushort n, CartaHelperBriscola h)
        {
            for (UInt16 i = 0; i < n; i++)
            {
                carte[i] = new Carta(i, h);

            }
            CaricaImmagini(m, n, h);
        }
        public static Carta GetCarta(UInt16 quale) { return carte[quale]; }
        public UInt16 GetSeme() { return seme; }
        public UInt16 GetValore() { return valore; }
        public UInt16 GetPunteggio() { return punteggio; }
        public string GetSemeStr() { return semeStr; }
        public bool StessoSeme(Carta c1) { if (c1 == null) return false; else return seme == c1.GetSeme(); }
        public int CompareTo(Carta c1)
        {
            if (c1 == null)
                return 1;
            else
                return helper.CompareTo(helper.GetNumero(GetSeme(), GetValore()), helper.GetNumero(c1.GetSeme(), c1.GetValore()));
        }

        public override string ToString()
        {
            return $"{valore + 1} di {semeStr}{(StessoSeme(helper.GetCartaBriscola()) ? "*" : " ")} ";
        }

        public static Bitmap GetImmagine(UInt16 quale)
        {
            return carte[quale].img;
        }

        public Bitmap GetImmagine()
        {
            return img;
        }

        public static void CaricaImmagini(Mazzo m, ushort n, CartaHelperBriscola helper)
        {
            for (UInt16 i = 0; i < n; i++)
            {
                carte[i].img = new Bitmap(AssetLoader.Open(new Uri($"resm:CBriscola_Demo.Resources.Images.n{i}.png")));
                carte[i].semeStr = helper.GetSemeStr(i);
            }
        }
    }
}
