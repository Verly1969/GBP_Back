using System;
using System.Collections.Generic;
using System.Text;

namespace GBP.Domain.Entities;

public class SecurityLog
{
    public int Id { get; set; }
    public required string IpAddress { get; set; }
    public required string EndPoint { get; set; }
    public DateTime DateAttempt { get; set; }
    public string? UserAgent { get; set; }
    public bool IsBanned { get; set; }
    public DateTime? StartBan { get; set; }
    public DateTime? EndBan { get; set; }
    public string? BanRaison { get; set; }
    public string? CreatedBy { get; set; }
}
