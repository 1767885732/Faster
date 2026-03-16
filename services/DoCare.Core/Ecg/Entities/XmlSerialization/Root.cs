using System;
using System.Collections.Generic;
using System.Text;

namespace DoCare.Core.Ecg.Entities.XmlSerialization
{
    public class Request
    {
        /// <summary>
        /// Header
        /// </summary>
        public Header Header { get; set; }

        /// <summary>
        /// Body
        /// </summary>
        public Body Body { get; set; }
    }

    public class Header
    {
        /// <summary>
        /// 
        /// </summary>
        public string SourceSystem { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string MessageID { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string CreateTime { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string MethodName { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string ReqIimRules { get; set; }
    }

    public class Body
    {
        /// <summary>
        /// 
        /// </summary>
        public string HospCode { get; set; }

        /// <summary>
        /// 靖江市人民医院
        /// </summary>
        public string HospDesc { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string PatientID { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string EpisodeID { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string RISRExamID { get; set; }

        /// <summary>
        /// 心电图检查报告
        /// </summary>
        public string ReportDesc { get; set; }

        /// <summary>
        /// 发布
        /// </summary>
        public string RepStatus { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string SpecimenID { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string OrderItemID { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string DocType { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string DocID { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string DocContent { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string DocPath { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string PictureUrl { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string ExamDocCode { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string ExamDocName { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string RepDocCode { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string RepDocName { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string VitiDocCode { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string VitiDocName { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string ExamTime { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string RepTime { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string VitiTime { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string ResultID { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string ResultCon { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string AbnoStatus { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string AbnoContent { get; set; }

        /// <summary>
        /// RepResultList
        /// </summary>
        public List<RepResult> RepResultList { get; set; }
    }

    public class RepResult
    {
        /// <summary>
        /// 
        /// </summary>
        public string ExamItem { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string ExamResult { get; set; }
    }
}
