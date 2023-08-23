namespace DBwebAPI
{
    public enum ValidTokenAuthority
    {
        //  在验证token时使用到的参数类型
        //  Normal表示用户身法应该为普通用户
        //  none表示不验证
        Normal, Admin, None
    }
}
