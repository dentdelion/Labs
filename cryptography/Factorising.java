package cryptography;

import java.util.*;
import java.lang.*;

/**
 * Made by Elizabeth Blyumska, fall 2015. 
 * Factorisation and logarithmic algorithms: 
 * Pollard, Ferma, Quadric, Lenstra;
 * Shanks, Pollard, Poligue-Helman, Primitive.
 */

class Crypt {
	public static void main(String[] args) {
		Scanner s = new Scanner(System.in);
			System.out.println("Hello! Please, choose the mode you want to test: (quit - \"quit\"\n");
			System.out.println("1 - Factorisation, \n2 - Logarithm;");
			switch(s.nextInt()) {
				case (1) : 	factChoice();
							break;
				case (2) : 	logChoice();
							break;
				default : return;
			}
		
	}

	public static void factChoice() {
		Scanner s = new Scanner(System.in);
		System.out.println("Please, choose again the mode you want to test:\n");
		for (int i = 0; i< factModesAmount; i++) {
			System.out.println((i+1) + " - " + modesFact[i] + ";");
		}
		int mode = s.nextInt();
		switch(mode) {
			case(1): 	choiceFact = new Pollard();
						break;
			case(2) : 	choiceFact = new Ferma();
						break;
			case(3): 	choiceFact = new Quadric();
						break;
			case(5) : 	choiceFact = new Lenstra();
						break;
			default:	return;
		}
		System.out.println("Please, input the number:");
		number = s.nextInt();
		long[] result = choiceFact.factorize(number);
		System.out.println("");
		for (int i = 0; i< result.length; i++) {
			System.out.print(result[i] + ", ");
		}
		System.out.print(" and that's all!\n");
	}

	public static void logChoice() {
		Scanner s = new Scanner(System.in);
		System.out.println("Please, choose again the mode you want to test:\n");
		for (int i = 0; i< logModesAmount; i++) {
			System.out.println((i+1) + " - " + modesLog[i] + ";");
		}
		int mode = s.nextInt();
		switch(mode) {
			case(1): 	choiceLog = new Shanks();
						break;
			case(2) : 	choiceLog = new PollardLog();
						break;
			case(3) : 	choiceLog = new PoligueHelman();
						break;
			case(4) : 	choiceLog = new Primitive();
						break;
			default :	return;
		}
		System.out.println("Please, input the base:");
		base = s.nextInt();
		System.out.println("Please, input the number:");
		number = s.nextInt();
		System.out.println("Please, input the modulo:");
		modulo = s.nextInt();
		long result = choiceLog.execute(base, number, modulo);
		System.out.println("");
		System.out.println(result);
	}
	public static String[] modesFact = {"pollard", "ferma", "quadric", "lenstra" };
	public static String[] modesLog = {"shanks", "pollard", "poligue-helman", "primitive"};
	public static int factModesAmount = 5;
	public static int logModesAmount = 6;
	public static Factorisation choiceFact;
	public static Logarithm choiceLog;
	public static long number, base, modulo;
}


