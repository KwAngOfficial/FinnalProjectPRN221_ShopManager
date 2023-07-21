using System;
using System.Collections.Generic;

namespace FinnalProjectPRN221_ShopManager.Model;

public partial class Banner
{
    public int BannerId { get; set; }

    public byte[]? Image { get; set; }

    public int? Active { get; set; }
}
