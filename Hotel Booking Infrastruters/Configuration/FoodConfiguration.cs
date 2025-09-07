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
					new Food() { Id = 1,  Name="چلو کباب", Description="برنج ایرانی معطر همراه با ۲ سیخ کباب کوبیده گوسفندی و مخلفات", Picture="koobide.jpg"},
					new Food() { Id = 2,  Name = "قورمه سبزی", Description = "خورش اصیل ایرانی با سبزی تازه، لوبیا قرمز و گوشت گوسفندی", Picture="qormesabzi.jpg"},
					new Food() { Id = 3,  Name = "فسنجان", Description = "خورش سنتی با گوشت قلقلی، گردوی آسیاب شده و رب انار ترش و شیرین", Picture="fesenjan-goosht-ghelgheli.jpg"},
					new Food() { Id = 4,  Name = "دیزی", Description = "آبگوشت سنتی ایرانی با نخود، لوبیا، سیب‌زمینی و گوشت گوسفندی", Picture="Abgoosht.jpg"},
					new Food() { Id = 5,  Name = "قیمه", Description = "خورش لذیذ با لپه، سیب‌زمینی سرخ‌شده و گوشت گوسفندی", Picture="qeyme.jpg"},
					new Food() { Id = 6,  Name = "زرشک پلو با مرغ", Description = "برنج زعفرانی همراه با مرغ سرخ‌شده و زرشک تازه", Picture="zereshk-polo-morgh.jpg"},
					new Food() { Id = 7,  Name = "باقالی پلو با ماهیچه", Description = "پلو معطر با شوید و باقالی به همراه گوشت ماهیچه گوسفندی", Picture="baghali-polo-ba-mahicheh.jpg"},
					new Food() { Id = 8,  Name = "ته چین", Description = "لایه‌های برنج زعفرانی با مرغ، ماست و زرده تخم‌مرغ، با ته‌دیگ طلایی", Picture="Tachin.jpg"},
					new Food() { Id = 9,  Name = "جوجه کباب", Description="فیله مرغ مزه‌دارشده و کبابی با برنج ایرانی و گوجه کبابی", Picture = "jojekabab.jpg"},
					new Food() { Id = 10, Name = "رشته پلو", Description = "برنج ایرانی همراه با رشته پلویی، کشمش و گوشت خورشتی", Picture = "reshteh-polo-gosht-min.jpg"},
					new Food() { Id = 11, Name = "کشک بادمجان", Description = "ترکیب بادمجان سرخ‌شده، کشک، نعناع داغ و گردوی خرد شده", Picture="kashk-bademjan-kababi.jpg"},
					new Food() { Id = 12, Name = "مرغ بریانی", Description = "مرغ پخته‌شده با ادویه‌های مخصوص هندی و برنج معطر", Picture="beryani.jpg"},
					new Food() { Id = 13, Name = "باقالی پلو با مرغ", Description = "پلو با شوید و باقالی تازه همراه با مرغ سرخ‌شده", Picture="baghali-polo-morgh-1.jpg"},
					new Food() { Id = 14, Name = "پیتزا مخلوط", Description = "پیتزا با گوشت چرخ‌کرده، قارچ، فلفل دلمه‌ای و پنیر کشدار", Picture="makhloot.jpg"},
					new Food() { Id = 15, Name = "پیتزای مخصوص", Description = "پیتزا با مرغ، قارچ، گوشت دودی و پنیر موزارلا", Picture="makhsoos.jpg"},
					new Food() { Id = 16, Name = "پیتزای پپرونی", Description = "پیتزای کلاسیک با پپرونی تند، پنیر و سس گوجه‌فرنگی", Picture="pizzapeperoni.jpg"},
					new Food() { Id = 17, Name = "پیتزای سیر استیک", Description = "پیتزا با تکه‌های استیک گوشت، سیر تازه و پنیر کشدار", Picture="pizzasirestake.jpg"},
					new Food() { Id = 18, Name = "پیتزای ایتالیایی", Description = "مارگاریتا با گوجه تازه، ریحان سبز و پنیر موزارلا", Picture="pizzaitaly.jpg"},
					new Food() { Id = 19, Name = "پیتزا سبزیجات", Description = "پیتزا گیاهی با قارچ، ذرت، زیتون، فلفل دلمه‌ای و پنیر", Picture="pizzasabsijat.jpg"},
					new Food() { Id = 20, Name = "پیتزای آمریکایی", Description = "پیتزا با گوشت چرخ‌کرده، سس مخصوص و پنیر زیاد", Picture="pizzaamerican.jpg"},
					new Food() { Id = 21, Name = "پیتزای سوجوک", Description = "پیتزا ترکی با سوسیس سوجوک، سبزیجات و پنیر", Picture="pizaasojock.jpeg"},
					new Food() { Id = 22, Name = "پاستا آلفردو", Description = "پاستا با سس آلفردو، مرغ گریل‌شده و قارچ تازه", Picture="pastaalfredo.jpg"},
					new Food() { Id = 23, Name = "لازانیا گوشت", Description = "لازانیا با گوشت چرخ‌کرده، سس بشامل و پنیر فراوان", Picture="lazania.jpg"},
					new Food() { Id = 24, Name = "چیکن استراگانوف", Description = "مرغ خلالی با قارچ، پیاز و سس خامه‌ای", Picture="chiken-estraganof.jpg"},
					new Food() { Id = 25, Name = "مرغ سوخاری", Description = "مرغ ترد سوخاری‌شده با ادویه مخصوص و سیب‌زمینی سرخ‌کرده", Picture="kentaki.jpg"},
					new Food() { Id = 26, Name = "سالاد", Description="سالاد سبزیجات تازه با سس مخصوص", Picture="salad.jpg"},
					new Food() { Id = 27, Name = "سالاد سزار", Description = "کاهو تازه، مرغ گریل‌شده، پنیر پارمسان و نان برشته", Picture="sezar.jpg"},
					new Food() { Id = 29, Name = "آب", Description = "آب معدنی خنک و تازه", Picture="water.png"},
					new Food() { Id = 30, Name = "نوشابه مشکی", Description = "نوشابه گازدار با طعم کولا", Picture="kok.jpg"},
					new Food() { Id = 31, Name = "نوشابه زرد", Description = "نوشابه گازدار با طعم پرتقال", Picture="fanta.jpg"},
					new Food() { Id = 32, Name = "اسپرایت", Description = "نوشابه گازدار با طعم لیمو", Picture="sprite.jpg"},
					new Food() { Id = 33, Name = "دوغ", Description = "دوغ سنتی ایرانی با نعناع", Picture="doq.jpg"},
					new Food() { Id = 34, Name="لوبیا پلو", Description="پلو ایرانی با لوبیا سبز، گوشت و رب گوجه", Picture="loobia-polo.jpg"},
					new Food() { Id = 35, Name="ماکارونی", Description="پاستای ایرانی با سس گوشت و رب گوجه", Picture="makarani.jpg"},
					new Food() { Id = 36, Name="دلمه", Description="دلمه برگ مو با برنج، سبزیجات و گوشت چرخ‌کرده", Picture="dolme-barg-mo.jpg"},
					new Food() { Id = 37, Name="سبزی پلو با ماهی", Description="پلو سبزی‌جات معطر همراه با ماهی سرخ‌شده یا کبابی", Picture="sabzi-polo-ba-mahi.jpg"}
			});
		}
	}
}
