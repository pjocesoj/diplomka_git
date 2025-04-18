﻿using MainNode.Communication.Enums;

namespace MainNode.Communication.Dto
{
    public class EndPointDto
    {
        public HttpMethodEnum HTTP { get; set; }
        public EndPointType Type { get; set; }
        public string URL { get; set; } = "";
        public ValueDto[] Vals { get; set; } = new ValueDto[0];
        public ValueArgDto[] Args { get; set; } = new ValueArgDto[0];
        public int? Delay { get; set; }
    }
}
