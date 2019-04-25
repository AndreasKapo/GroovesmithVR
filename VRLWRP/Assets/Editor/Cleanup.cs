//----------------------------------------------
//              Heavy-Duty Editor
//      Copyright © 2014 - 2018  Illogika
//----------------------------------------------
using UnityEngine;
using UnityEditor;
using System.IO;

[InitializeOnLoad]
public static class Cleanup
{
	private const string TOP_MENU = "Tools/";
	private const string SUB_MENU = "Cleanup/";

    #region MenuConstants
    private const string REMOVE_EMPTY_DIRECTORIES_MENU_ITEM = "Remove Empty Directories";
	private const string REMOVE_FILES_WITH_EXTENSIONS_MENU_ITEM = "Remove all files with specific extensions and their .meta files.";

	private const string AUTO_DIRECTORY_CLEANUP_ON_MENU_ITEM = "Turn Auto Directory Cleanup On";
	private const string AUTO_DIRECTORY_CLEANUP_OFF_MENU_ITEM = "Turn Auto Directory Cleanup Off";
	private const string AUTO_FILE_CLEANUP_ON_MENU_ITEM = "Turn Auto File Cleanup On";
	private const string AUTO_FILE_CLEANUP_OFF_MENU_ITEM = "Turn Auto File Cleanup Off";

	private const int AUTO_DIRECTORY_CLEANUP_ON_ORDER = 100;
	private const int AUTO_DIRECTORY_CLEANUP_OFF_ORDER = 101;
	private const int AUTO_FILE_CLEANUP_ON_ORDER = 102;
	private const int AUTO_FILE_CLEANUP_OFF_ORDER = 103;
	#endregion

	private const string AUTO_CLEANUP_DIRECTORY_KEY = "HDE_AUTO_DIRECTORY_CLEANUP";
	private const string AUTO_CLEANUP_FILE_KEY = "HDE_AUTO_FILE_CLEANUP";

	static Cleanup()
	{
		if(EditorPrefs.HasKey(AUTO_CLEANUP_DIRECTORY_KEY) && EditorPrefs.GetBool(AUTO_CLEANUP_DIRECTORY_KEY))
		{
			CleanUpDirectoriesRecursive(Path.Combine(Directory.GetCurrentDirectory(), "Assets"));
		}

		AssetDatabase.Refresh();
	}

	[MenuItem(TOP_MENU + SUB_MENU + REMOVE_EMPTY_DIRECTORIES_MENU_ITEM, false/*, REMOVE_EMPTY_DIRECTORIES_ORDER*/)]
	public static void RemoveEmptyDirectories()
	{
		if(EditorUtility.DisplayDialog(REMOVE_EMPTY_DIRECTORIES_MENU_ITEM, "This will remove all empty directories and their meta files from the project.", "Cleanup", "Cancel"))
		{
			int emptyDirectoriesRemoved = CleanUpDirectoriesRecursive(Path.Combine(Directory.GetCurrentDirectory(), "Assets"));
			Debug.Log(string.Format("Removed {0} empty directories.", emptyDirectoriesRemoved));
			AssetDatabase.Refresh();
		}
	}

	public static int CleanUpDirectoriesRecursive(string path)
	{
		int emptyDirectoriesRemoved = 0;
		foreach(string directory in Directory.GetDirectories(path))
		{
			emptyDirectoriesRemoved += CleanUpDirectoriesRecursive(directory);
		}

		if(Directory.GetFiles(path, "*", SearchOption.AllDirectories).Length == 0)
		{
			++emptyDirectoriesRemoved;
			Directory.Delete(path);
			File.SetAttributes(path + ".meta", File.GetAttributes(path + ".meta") & ~FileAttributes.ReadOnly);
			File.Delete(path + ".meta");
		}
		return emptyDirectoriesRemoved;
	}

	[MenuItem(TOP_MENU + SUB_MENU + AUTO_DIRECTORY_CLEANUP_ON_MENU_ITEM, false, AUTO_DIRECTORY_CLEANUP_ON_ORDER)]
	public static void TurnAutoCleanupOn()
	{
		EditorPrefs.SetBool(AUTO_CLEANUP_DIRECTORY_KEY, true);
	}

	[MenuItem(TOP_MENU + SUB_MENU + AUTO_DIRECTORY_CLEANUP_ON_MENU_ITEM, true, AUTO_DIRECTORY_CLEANUP_ON_ORDER)]
	public static bool TurnAutoCleanupOnValidator()
	{
		return !EditorPrefs.HasKey(AUTO_CLEANUP_DIRECTORY_KEY) || !EditorPrefs.GetBool(AUTO_CLEANUP_DIRECTORY_KEY);
	}

	[MenuItem(TOP_MENU + SUB_MENU + AUTO_DIRECTORY_CLEANUP_OFF_MENU_ITEM, false, AUTO_DIRECTORY_CLEANUP_OFF_ORDER)]
	public static void TurnAutoCleanupOff()
	{
		EditorPrefs.SetBool(AUTO_CLEANUP_DIRECTORY_KEY, false);
	}

	[MenuItem(TOP_MENU + SUB_MENU + AUTO_DIRECTORY_CLEANUP_OFF_MENU_ITEM, true, AUTO_DIRECTORY_CLEANUP_OFF_ORDER)]
	public static bool TurnAutoCleanupOffValidator()
	{
		return EditorPrefs.HasKey(AUTO_CLEANUP_DIRECTORY_KEY) && EditorPrefs.GetBool(AUTO_CLEANUP_DIRECTORY_KEY);
	}

	[MenuItem(TOP_MENU + SUB_MENU + AUTO_FILE_CLEANUP_ON_MENU_ITEM, false, AUTO_FILE_CLEANUP_ON_ORDER)]
	public static void TurnAutoFileCleanupOn()
	{
		EditorPrefs.SetBool(AUTO_CLEANUP_FILE_KEY, true);
	}

	[MenuItem(TOP_MENU + SUB_MENU + AUTO_FILE_CLEANUP_ON_MENU_ITEM, true, AUTO_FILE_CLEANUP_ON_ORDER)]
	public static bool TurnAutoFileCleanupOnValidator()
	{
		return !EditorPrefs.HasKey(AUTO_CLEANUP_FILE_KEY) || !EditorPrefs.GetBool(AUTO_CLEANUP_FILE_KEY);
	}

	[MenuItem(TOP_MENU + SUB_MENU + AUTO_FILE_CLEANUP_OFF_MENU_ITEM, false, AUTO_FILE_CLEANUP_OFF_ORDER)]
	public static void TurnAutoFileCleanupOff()
	{
		EditorPrefs.SetBool(AUTO_CLEANUP_FILE_KEY, false);
	}

	[MenuItem(TOP_MENU + SUB_MENU + AUTO_FILE_CLEANUP_OFF_MENU_ITEM, true, AUTO_FILE_CLEANUP_OFF_ORDER)]
	public static bool TurnAutoFileCleanupOffValidator()
	{
		return EditorPrefs.HasKey(AUTO_CLEANUP_FILE_KEY) && EditorPrefs.GetBool(AUTO_CLEANUP_FILE_KEY);
	}
	
}
