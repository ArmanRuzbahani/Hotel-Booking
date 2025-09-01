using Hotel_Booking_Domain.Core.Entitys;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hotel_Booking_Infrastruters.Configuration
{
	public class FoodConfiguration : IEntityTypeConfiguration<Food>
	{
		public void Configure(EntityTypeBuilder<Food> builder)
		{
			builder.HasData(new List<Food>()
			{ 
		    new Food() { Id = 1,Name="چلو کباب",Description="با برنج ایرانی مخلفات و 200 گرم گوشت گوسفندی",Picture=""},
			new Food() { Id = 2, Name = "قورمه سبزی", Description = "خورشتی ایرانی با سبزیجات، گوشت و لوبیا",Picture="" },
			new Food() { Id = 3, Name = "فسنجان", Description = "خورشت با گوشت و سس گردو و رب انار",Picture="" },
			new Food() { Id = 4, Name = "دیزی", Description = "خوراک سنتی ایرانی با گوشت گوسفند و سبزیجات",Picture="" },
			new Food() { Id = 5, Name = "قیمه", Description = "برنج ایرانی مخلفات",Picture=""},
			new Food() { Id = 6, Name = "زرشک پلو با مرغ", Description = "برنج زعفرانی همراه با مرغ و زرشک" , Picture = ""},
			new Food() { Id = 7, Name = "باقالی پلو با ماهیچه", Description = "پلو با باقالی و گوشت ماهیچه" , Picture = ""},
			new Food() { Id = 8, Name = "ته چین", Description = "پلو با مرغ و زعفران و ماست" , Picture = ""},
			new Food() { Id = 9, Name = "جوجه کباب",Description="برنج ایرانی 200 گرم جوجه سینه", Picture = ""},
			new Food() { Id = 10, Name = "رشته پلو", Description = "پلو با رشته، گوشت و زعفران" , Picture = ""},
			new Food() { Id = 11, Name = "کشک بادمجان", Description = "غذای ساده و خوشمزه با بادمجان و کشک",Picture="" },
			new Food() { Id = 12, Name = "مرغ بریانی", Description = "مرغ پخته شده با ادویه‌های خاص",Picture="" },
			new Food() { Id = 13, Name = "باقالی پلو با مرغ", Description = "پلو با باقالی و گوشت",Picture="" },
			new Food() { Id = 14, Name = "پیتزا مخلوط", Description = "پیتزا با ترکیب مختلف مواد اولیه مانند قارچ، گوشت، فلفل دلمه‌ای و پنیر",Picture="" },
			new Food() { Id = 15, Name = "پیتزای مخصوص", Description = "پیتزا با ترکیب مواد خاص مانند گوشت مرغ، قارچ و پنیر موزارلا",Picture="" },
			new Food() { Id = 16, Name = "پیتزای پپرونی", Description = "پیتزا با تکه‌های پپرونی، پنیر و سس گوجه‌فرنگی",Picture="" },
			new Food() { Id = 17, Name = "پیتزای سیر استیک", Description = "پیتزا با گوشت استیک، سیر و پنیر" ,Picture=""},
			new Food() { Id = 18, Name = "پیتزای ایتالیایی", Description = "پیتزای کلاسیک ایتالیایی با ترکیب ریحان تازه، گوجه‌فرنگی، پنیر موزارلا و روغن زیتون" , Picture = ""},
			new Food() { Id = 19, Name = "پیتزا سبزیجات", Description = "پیتزا با انواع سبزیجات تازه مانند قارچ، فلفل دلمه‌ای، زیتون و پنیر" ,Picture="" },
			new Food() { Id = 20, Name = "پیتزای امریکایی", Description = "پیتزا با گوشت چرخ کرده، پنیر موزارلا، فلفل دلمه‌ای و سس گوجه‌فرنگی" ,Picture=""},
			new Food() { Id = 21, Name = "پیتزای سوجوک", Description = "پیتزا با سوجوک (سوسیس ترکی) به همراه پنیر و سبزیجات" ,Picture="" },
			new Food() { Id = 22, Name = "پاستا الفردو", Description = "پیتزا با سس آلفردو، مرغ پخته و پنیر" , Picture = ""},
			new Food() { Id = 23, Name = "لازانیا گوشت", Description = "لازانیا با لایه‌های نودل، گوشت چرخ کرده، پنیر و سس بشامل" , Picture = ""},
			new Food() { Id = 24, Name = "چیکن استراناگوف", Description = "مرغ پخته شده با سس خامه‌ای، پیاز و قارچ" , Picture = ""},
			new Food() { Id = 25, Name = "مرغ سخاری", Description = "مرغ تکه‌ای پخته شده با طعم‌های مختلف و پوشش داغ و ترد از آرد و ادویه" , Picture = ""},
			new Food() { Id = 26, Name = "سالاد", Description="کاهو گوجه خیار سس مخصوص" , Picture = ""},
			new Food() { Id = 27, Name = "سالاد سزار", Description = "کاهو مرغ گریل شده" , Picture = ""},
			new Food() { Id = 29, Name = "آب", Description = "بزرگ" , Picture = ""},
			new Food() { Id = 30, Name = "نوشابه مشکی", Description = "بزرگ" , Picture = ""},
			new Food() { Id = 31, Name = "نوشابه زرد", Description = "بزرگ" , Picture = ""},
			new Food() { Id = 32, Name = "اسپرایت", Description = "بزرگ" , Picture = ""},
			new Food() { Id = 33, Name = "دوغ", Description = "بزرگ" , Picture = ""},
			new Food() { Id = 34, Name = "آب گازدار", Description = "بزرگ" ,Picture=""},
			});
		}
	}
}
