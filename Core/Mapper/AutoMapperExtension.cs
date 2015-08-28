namespace Core.Mapper
{
    /// <summary>
    /// AutoMapper相關的Extension方法
    /// </summary>
    public static class AutoMapperExtension
    {
        /// <summary>
        /// 把目前的Model透過AutoMapper轉成某一個Type的Model。
        /// AutoMapper需要先註冊過這個轉換的設定。
        /// </summary>
        /// <typeparam name="TDestination">要轉換成的Type</typeparam>
        /// <param name="source">要被轉換的object</param>
        /// <returns>轉換成為Type的Object</returns>
        public static TDestination ToModel<TDestination>(this object source)
        {
            return AutoMapper.Mapper.Map<TDestination>(source);
        }
    }
}
