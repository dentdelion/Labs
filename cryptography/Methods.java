package cryptography;
import java.util.Random;
interface Factorisation {
	long[] factorize(long a);
}


class Pollard implements Factorisation {
	public boolean isIn (long a, long[] array) {
		for (int i = 0; i< array.length; i++) {
			if (a == array[i]) return true;
		}
		return false;
	}

	public long[] factorize(long number) { //F = x^2 + 1
		long[] a = new long[50];
		long[] b = new long[50];
		long result = 1;
		long[] res = new long[2];
		for (int j = 0; j < 50; j++) {
			a[j] = 0;
			b[j] = 0;
		}
		b[0] = 2;
		a[0] = 2; 
		long ai, bi, gcd;
		for (int i = 1; i < 50; i++) {
			System.out.println(i);
			ai = (a[i-1] * a[i-1] + 1) % number;
			bi = (b[i-1] * b[i-1] + 1) % number;
			bi = (bi * bi + 1) % number;
			if (isIn(ai, a) || isIn(bi, b)) {
				isCycle = true;
				break;
			} 
			a[i] = ai;
			b[i] = bi;
			if (bi - ai < 0) 
				bi+=number;
			gcd = gcdE(bi - ai, number);

			if (gcd != 1 && gcd != number) {
				result = gcd;
				break;
			}
		}
		res[0] = result;
		res[1] = number / result;
		return res;
	}

	public static long gcdE(long a, long b) {
	    if (a == 0)
	        return b;
	    System.out.println(a + " " + b);
	    while (b > 0 && a > 0) {
	        if (a > b)
	            a = a - b;
	        else
	            b = b - a;
	    }
		return a;
	}
	public boolean isCycle;
}

class Ferma implements Factorisation {
	public static long isSquare(long a) {
		int a1 = (int)(java.lang.Math.sqrt((double)a));
		for (int i = 1; i<a1 + 1; i++) {
			if (a == i * i) 
				return i;
		}
		return -1;
	}
	public long[] factorize(long a) {
		long[] result = new long[2];
		long y2 = 1;
		long y;
		long k = (long)java.lang.Math.sqrt((double)a);
		for (long i = k; i < (a + 1)/2; i++) {
			y2 = i*i - a;
			y = isSquare(y2);
			if (y!=-1) {
				result[0] = i - y;
				result[1] = i + y;
				return result;
			}
		}
		return result;
	}

}

class Quadric implements Factorisation {

	public long[] factorize(long n) {
		long r0 = (long)java.lang.Math.sqrt((double)n);
		//if (n % 4 == 1) n = 2 * n;
		long d1 = 4*n;
		long q0 = (long)java.lang.Math.sqrt((double)d1);
		long[] p = new long[100];
		long[] q = new long[100];
		long[] r = new long[100];
		long[] p1 = new long[100];
		long[] q1 = new long[100];
		long[] r1 = new long[100];

		long[] res = new long[2];
		p[0] = 0;
		q[0] = 1;
		r[0] = r0;
		p[1] = r0;
		q[1] = n - r0*r0;
		r[1] = (long)((double)(2*r0)/q[1]);
		long result, d;
		int k = 1;
		d = 1;
		for (int i = 2; i < 100; i++) {
			p[i] = r[i-1] * q[i - 1] - p[i-1];
			q[i] = q[i-2] + (p[i-1] - p[i]) * r[i-1];
			r[i] = (long)((double)(p[i] + r0) / q[i]);
			if (Ferma.isSquare(q[i]) != -1) {
				k = i;
				d = Ferma.isSquare(q[i]);
				break;
			}
		}
		p1[0] = -p[k];
		q1[0] = d;
		r1[0] = (long)((p1[0] + r0)/q1[0]);
		p1[1] = r1[0] * q1[0] - p1[0];
		q1[1] = (long)((double)(n - p1[1] * p1[1])/ q1[0]);
		r1[1] = (long)((double)(p1[0] + r0)/q1[1]);
		result = 1;
		for (int i = 2; i < 100; i++) {
			p1[i] = r1[i-1] * q1[i - 1] - p1[i-1];
			q1[i] = q1[i-2] + (p1[i-1] - p1[i]) * r1[i-1];
			r1[i] = (long)((double)(p1[i] + r0) / q1[i]);
			if (p1[i] == p1[i-1]) {
				result = q1[i-1]; 
				break;
			}
		}
		res[0] = result;
		res[1] = n / result;

		return res;
	}
}


class Lenstra implements Factorisation {
	public long inverse(long number, long modulo) { //обратное по модулю
		while (number >= modulo)
			number %=modulo;
		long[] r = new long[(int)number];
		r[1] = 1;
		for (int i=2; i<number; ++i) {
			r[i] = (modulo - (modulo / i) * r[(int)(modulo % i)] % modulo) % modulo;
		}
		return r[(int)(number - 1)];
	}

	public long dydxInADot(long a, long modulo, long x, long y) { //производная эллиптической кривой в точке
		long dy = (long)((double)(3*x*x + a)/(2*y)); //a *x^3 + b*x +c = y^2, x, y - координаты точки
		if (Pollard.gcdE(2*y, modulo) != 1) 
			return -1;
		long dx = inverse(2*y, modulo);
		return dy * dx;
	}

	public long fact(long a) { //a!
		int result = 1;
		for (int i = 1; i <= a; i++) {
			result*=i;
		}
		return result;
	}


	public boolean check(long x, long y, long a, long b) { //проверка, принадлежит ли точка эллиптической кривой
		return ((x*x*x + a*x + b) == (y*y));
	}

	public long[] grouprule(long x, long x0, long y, long k, long s) {
		long xtemp;
		long[] result = new long[2];
		for (int i = 0; i < k; i++) {
			xtemp = x;
			x = (s*s - x - x0);
			y = (-1*y + s*(xtemp - x));
		}
		result[0] = x;
		result[1] = y;
		return result;
	}

	public long[] factorize(long modulo) {
		//let's find elliptic curve
		Random r = new Random(System.currentTimeMillis());
		System.out.println("This is our elliptic curve we're going to use:\n");
		long a = -5;//r.nextInt(10) + 1;
		long b = 5;//r.nextInt(10) + 1;
		System.out.println("x^3 + "+a+"x + " + b);
		long k = 10;
		long[] resultFinal = new long[2];
		long[] px = new long[(int)k];
		long[] py = new long[(int)k];
		px[0] = 1;
		py[0] = px[0]*px[0]*px[0] + a*px[0] + b;
		System.out.println("This is P0: ("+px[0]+", "+py[0]+")");
		long s = 1;
		for (int i = 1; i < k; i++) {
			s = dydxInADot(a, modulo, px[i-1], py[i-1]);
			if (s == -1) {
				resultFinal[0] = py[i-1];
				resultFinal[1] = modulo / py[i-1];
				break;
			}
			long[] result = new long[2];
			result = grouprule(px[i-1], px[0], py[i-1], fact(i + 1), s);
			if (check(result[0], result[1], a, b)) {
				System.out.println("This is P" + i + ": ("+px[i]+", "+py[i]+")");
				px[i] = result[0] % modulo;
				py[i] = result[1] % modulo;
			} else {
				System.out.println("ERROR");
				break;
			}
		}
		return resultFinal;
	}
}

