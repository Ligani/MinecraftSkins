namespace MinecraftSkins.Services.DTO
{
        public class PagedResponse<T>
        {
            public List<T> Items { get; set; }
            public int TotalCount { get; set; }
            public int PageNumber { get; set; }
            public int PageSize { get; set; }

            public int TotalPages
            {
                get
                {
                    if (PageSize == 0) return 0;
                    return (int)Math.Ceiling(TotalCount / (double)PageSize);
                }
            }
        }
    }
