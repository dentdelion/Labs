package cryptography;
import java.lang.Math.*;
import java.util.Random;
interface Logarithm {
	long execute(long base, long number, long modulo);
}

class Shanks implements Logarithm {
	public long[] findInTwoArrays(long[] a, long[] b) {
		long[] indexes = new long[2];
		for (int i = 0; i< a.length; i++) {
			for (int j = 0; j< b.length; j++){
				if (a[i] == b[j]) {
					indexes[0] = i;
					indexes[1] = j;
					return indexes;
				}
			}
		}
		indexes[0] = 0;
		indexes[1] = 0;
		return indexes;
	}
	public long execute (long base, long number, long modulo) { //g , b, n
		long a = (long)(java.lang.Math.sqrt((double)modulo)) + 1;
		long l1[] = new long[(int)a + 1];
		long l2[] = new long[(int)a];
		long baseInPowerA = 1;
		for (int i = 0; i < a; i++) {
			baseInPowerA *= base;
		}
		System.out.println("BASE IN POWER A\t" + baseInPowerA);
		l1[0] = 1;
		l2[0] = number;

		for (int i = 1, j = 1; i < a + 1 && j < a; i++, j++) {
			l1[i] = (l1 [i - 1] * baseInPowerA) % modulo;
			l2[j] = (l2[j - 1] * base) % modulo;

			System.out.println("l1  " + (i) + "   " + l1[i]);
			System.out.println("l2  " + (j) + "   " + l2[j]);
		}
		long[] iz = findInTwoArrays(l1, l2);
		long k = iz[1];
		long l = iz[0];

		//(b * g ^ iz[1] = g ^ (a*iz[0]))
		return l*a - k;
	} 
}

class PollardLog implements Logarithm {
	public long euler(long n) {
		long res = 1;
		for (int i = 2; i * i <= n; i++) {
			long p = 1;
			while (n % i == 0) {
				n/=i;
				p*=i;
			}
			p/=i;
			if (p!=0) 
				res *= (p*(i-1));
		}
		long k = n - 1;
		if (k == 0)
			return res;
		else
			return k*res;
	}

	public long[] new_xab(long number, long base, long modulo, long x1, long a1, long b1) {
		switch ((int)x1 % 3) {
			case (2): {// (x1 < modulo / 3) {
				x1 = number * x1 % modulo;
				a1 = a1;
				b1 = (b1 + 1) % modulo;
				break;
			}
			case (0): {//if (x1 >= modulo / 3 && x1 < 2*modulo/3) {
				x1 = (x1 * x1) % modulo;
				a1 = (2 * a1) % modulo;
				b1 = (2 * b1) % modulo;
				break;
			}
			case (1):  {//if (x1 >= 2 * modulo / 3) {
				x1 = base * x1 % modulo;
				a1 = (a1 + 1) % modulo;
				b1 = b1;
				break;
			}
		}
		long[] res = new long[3];
		res[0] = x1;
		res[1] = a1;
		res[2] = b1;
		return res;
	}

	public long execute(long base, long number, long modulo) {
		long n = modulo;
		long a1 = 0;
		long a2 = 0;

		long b1 = 0;
		long b2 = 0;

		long x1 = 1;
		long x2 = 1;

		if (number == modulo)
			return 1;
		boolean start = true;
		int j = 1;
		long[] res = new long[3];
		long[] res1 = new long[3];
		long[] res2 = new long[3];
		while (x1 != x2 || start) {
			start = false;
			res = new_xab(number, base, modulo, x1, a1, b1);
			x1 = res[0];
			a1 = res[1];
			b1 = res[2];
			res1 = new_xab(number, base, modulo, x2, a2, b2);
			res2 = new_xab(number, base, modulo, res1[0], res1[1], res1[2]);
			x2 = res2[0];
			a2 = res2[1];
			b2 = res2[2];
			System.out.println("x [" + j + "]:\t" + x1 +"\tx [" + 2*j + "]:\t" + x2);
			j++;
		}
		long u = (a2 - a1);
		System.out.println("This is a1 - a2: " + u);
		long v = (b2 - b1);
		System.out.println("This is r = b1 - b2: " + v);
		System.out.println("This is inverse to r by 17: " + inverse(v, modulo));
		if (v % n == 0)
			return -2;
		long x = (( inverse(v, modulo) * u ) % modulo);
		return x;
	}
	
	public static long inverse(long number, long modulo) {
		long[] result = extended_euclid(number, modulo);
		if (result[0] == 1) 
			return result[1] % modulo;
		return 0;
	}
	public static long[] extended_euclid(long a, long b) {
		long q, r, x1, x2, y1, y2, x, y, d;
		if (b == 0) {
			d = a; 
			x = 1; 
			y = 0;
			return null;
		}
		x2 = 1; 
		x1 = 0; 
		y2 = 0;
		y1 = 1;
		while (b > 0) {
			q = a / b;
			r = a - q * b;
			x = x2 - q * x1;
			y = y2 - q * y1;
			a = b; 
			b = r;
			x2 = x1; 
			x1 = x; 
			y2 = y1;
			y1 = y;
		}
		d = a; 
		x = x2; 
		y = y2;
		long[] result = new long[2];
		result[0] = d;
		result[1] = x;
		return result;	
	}
}


class PoligueHelman implements Logarithm {
	public long baseInPowerA(long base, long power, long modulo) {
		if (power < 0) {
			base = PollardLog.inverse(base, modulo);
			power = -power;
		}
		if (modulo == -1) {
			long result = 1;
			for (int i = 0; i<power; i++) {
				result*=base;
			}
			return result;
		}
		long result = 1;
		for (int i = 0; i<power; i++) {
			result*=base;
		}
		return result % modulo;
	}

	public long execute (long base, long number, long modulo) {
		long q0 = baseInPowerA(number, (modulo-1)/2, modulo);
		long p1 = modulo - 1;
		long p = 0;
		while (p1 != 1) {
			p1 = p1 / 2;
			p++;
			if (p1 < 1) {
				System.out.println("Your p isn't 2^n + 1!");
				break;
			}
		}
		long[] q = new long[(int)(p)];
		long[] z = new long[(int)(p)];
		long[] m = new long[(int)(p)];
		m[0] = 0;

		for (int i = 1; i < p; i++) {
			m[i] = (modulo - 1)/baseInPowerA(2, i + 1, -1);
		}
		long k;
		z[0] = 0;
		q[0] = (baseInPowerA(number, (modulo - 1)/2, modulo) == 1) ? 0 : 1; 
		z[1] = number * baseInPowerA(base, -1*q[0], modulo) % modulo;
		if (baseInPowerA(z[1], (modulo - 1) / 4, modulo) == 1)
			q[1] = 0;
		else
			q[1] = 1;
		z[2] = z[1] * (baseInPowerA(base, -1 * q[1] * 2, modulo)) % modulo;
		for (int i = 2; i < p; i++) {
			z[i] = z[i-1] * baseInPowerA(base, (-1*q[i-1]*baseInPowerA(2, i-1, -1)), modulo) % modulo;
			k = baseInPowerA(z[i], m[i], modulo);
			if (k - modulo == -1) 
				k-=modulo;
			if (k == 1)
				q[i] = 0;
			else if (k == -1)
				q[i] = 1;
			else
				q[i] = -2;
		}
		long result = 0;
		for (int i = 0; i < p; i++) {
			result += baseInPowerA(2, i, -1)*q[i];
		}
		return result;
	} 
}

class Primitive implements Logarithm {
	public long execute (long base, long number, long modulo) {
		if (number % base != 0) return -1;
		long result = 0;
		long num = 1;
		while (num != number) {
			num *= base;
			result++;
		}
		return result;
	} 
}
