using NoticeBoard;

public class DES_zaowu
{
    #region 加密解密  

    public static DES dd = new DES();

    private static string StringKey = "1991731.";

    /// <summary>  
    /// 解密  
    /// </summary>  
    /// <param name="str"></param>  
    /// <returns></returns>  
    public static string Decder(string str)
    {
        return dd.DesDecrypt(str, StringKey);
    }

    /// <summary>  
    /// 加密  
    /// </summary>  
    /// <param name="str"></param>  
    /// <returns></returns>  
    public static string Encoder(string str)
    {
        return dd.DesEncrypt(str, StringKey);
    }

    #endregion
}
