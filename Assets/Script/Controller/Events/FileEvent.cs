public class FileEvent : EventObject
{
    public const string SAVE = "FileEvent_SAVE_CODE";
    public const string LOAD = "FileEvent_LOAD";

    public const string OPEN_PROJECT = "FileEvent_OPEN_PROJECT";
    public const string OPEN_PROJECT_FAIL = "FileEvent_OPEN_PROJECT_FAIL";
    public const string OPEN_PROJECT_SUCCESS = "FileEvent_OPEN_PROJECT_SUCCESS";
    public const string NEW_PROJECT = "FileEvent_NEW_PROJECT";
    public const string NEW_PROJECT_COMPLETE = "FileEvent_NEW_PROJECT_COMPLETE";
    public const string SAVE_PROJECT = "FileEvent_SAVE_PROJECT";

    public const string SAVE_THICKIRREGULAR = "FileEvent_SAVE_THICKIRREGULAR";
    public const string LOAD_THICKIRREGULAR = "FileEvent_LOAD_THICKIRREGULAR";
    public const string LOAD_THICKIRREGULAR_COMPLETE = "FileEvent_LOAD_THICKIRREGULAR_COMPLETE";

    public const string LOAD_OBJ = "FileEvent_LOAD_OBJ";
    public const string LOAD_OBJ_COMPLETE = "FileEvent_LOAD_OBJ_COMPLETE";

    public const string LOAD_TEXTURE = "FileEvent_LOAD_TEXTURE";
    public const string LOAD_TEXTURE_COMPLETE = "FileEvent_LOAD_TEXTURE_COMPLETE";

    public const string SAVE_COMBINATION = "FileEvent_SAVE_COMBINATION";
    public const string LOAD_COMBINATION = "FileEvent_LOAD_COMBINATION"; 
    public const string LOAD_COMBINATION_COMPLETE = "FileEvent_LOAD_COMBINATION_COMPLETE"; 

    public string url;
    public string name;
    public object obj;

    public FileEvent(string types, string url = "", string name = "", object obj = null)
        : base(types)
    {
        this.url = url;
        this.name = name;
        this.obj = obj;
    }
}
