
public class Globals
{
	public static int LastGameHull = 0;
	public static int LastGameOxygen = 0;
	public static int LastGameGold = 0;

	public static int Score {
		get {
			if(LastGameHull == 0 || LastGameOxygen == 0) {
				return 0;
			}
			else {
				return 9773*LastGameGold + 10*LastGameHull + 10*LastGameOxygen;
			}
		}
	}
}
