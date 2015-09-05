namespace Example.Models
{
    using System;
    using System.Threading;

    /// <summary>
    /// アプリケーション形態によらず使用されるコア処理
    /// </summary>
    public class CoreService
    {
        public int Wait { get; set; }

        /// <summary>
        ///
        /// </summary>
        public void Something()
        {
            Thread.Sleep(Wait);
        }
    }
}
