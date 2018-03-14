using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Questioning.Models
{
    public class Frame
    {
        public string Name { get; set; }

        public List<FrameRow> Rows {get;set;}
    }

    public class FrameRow
    {
        public Dictionary<string, object> Slots { get; set; }
    }

    public class QuestioningFrameProvider
    {
        public Frame GetFrame()
        {
            Frame q1 = new Frame
            {
                Name = "Переживаєте за успіх в роботі?",
                Rows = new List<FrameRow>
                {
                    new FrameRow
                    {
                        Slots = new Dictionary<string, object>
                        {
                            { "Відповідь", "Сильно." },
                            { "Оцінка", 5 },
                        }
                    },
                    new FrameRow
                    {
                        Slots = new Dictionary<string, object>
                        {
                            { "Відповідь", "Не дуже." },
                            { "Оцінка", 3 },
                        }
                    },
                    new FrameRow
                    {
                        Slots = new Dictionary<string, object>
                        {
                            { "Відповідь", "Спокійний." },
                            { "Оцінка", 2 },
                        }
                    }
                }
            };
            Frame q2 = new Frame
            {
                Name = "Прагнете досягти швидко результату?",
                Rows = new List<FrameRow>
                {
                    new FrameRow
                    {
                        Slots = new Dictionary<string, object>
                        {
                            { "Відповідь", "Поступово." },
                            { "Оцінка", 5 },
                        }
                    },
                    new FrameRow
                    {
                        Slots = new Dictionary<string, object>
                        {
                            { "Відповідь", "Якомога швидше." },
                            { "Оцінка", 3 },
                        }
                    },
                    new FrameRow
                    {
                        Slots = new Dictionary<string, object>
                        {
                            { "Відповідь", "Дуже." },
                            { "Оцінка", 2 },
                        }
                    }
                }
            };
            Frame q3 = new Frame
            {
                Name = "Легко попадаєте в тупик при проблемах в роботі?",
                Rows = new List<FrameRow>
                {
                    new FrameRow
                    {
                        Slots = new Dictionary<string, object>
                        {
                            { "Відповідь", "Неодмінно." },
                            { "Оцінка", 5 },
                        }
                    },
                    new FrameRow
                    {
                        Slots = new Dictionary<string, object>
                        {
                            { "Відповідь", "Поступово." },
                            { "Оцінка", 3 },
                        }
                    },
                    new FrameRow
                    {
                        Slots = new Dictionary<string, object>
                        {
                            { "Відповідь", "Зрідка." },
                            { "Оцінка", 2 },
                        }
                    }
                }
            };
            Frame q4 = new Frame
            {
                Name = "Чи потрібен чіткий алгоритм для вирішення задач?",
                Rows = new List<FrameRow>
                {
                    new FrameRow
                    {
                        Slots = new Dictionary<string, object>
                        {
                            { "Відповідь", "Так." },
                            { "Оцінка", 5 },
                        }
                    },
                    new FrameRow
                    {
                        Slots = new Dictionary<string, object>
                        {
                            { "Відповідь", "В окремих випадках." },
                            { "Оцінка", 3 },
                        }
                    },
                    new FrameRow
                    {
                        Slots = new Dictionary<string, object>
                        {
                            { "Відповідь", "Не потрібен." },
                            { "Оцінка", 2 },
                        }
                    }
                }
            };

            Frame q5 = new Frame
            {
                Name = "Чи використовуєте власний досвід при вирішенні задач?",
                Rows = new List<FrameRow>
                {
                    new FrameRow
                    {
                        Slots = new Dictionary<string, object>
                        {
                            { "Відповідь", "Зрідка." },
                            { "Оцінка", 5 },
                        }
                    },
                    new FrameRow
                    {
                        Slots = new Dictionary<string, object>
                        {
                            { "Відповідь", "Частково." },
                            { "Оцінка", 3 },
                        }
                    },
                    new FrameRow
                    {
                        Slots = new Dictionary<string, object>
                        {
                            { "Відповідь", "Ні." },
                            { "Оцінка", 2 },
                        }
                    }
                }
            };
            Frame q6 = new Frame
            {
                Name = "Чи користуєтесь фіксованими правилами для вирішення задач?",
                Rows = new List<FrameRow>
                {
                    new FrameRow
                    {
                        Slots = new Dictionary<string, object>
                        {
                            { "Відповідь", "Так." },
                            { "Оцінка", 5 },
                        }
                    },
                    new FrameRow
                    {
                        Slots = new Dictionary<string, object>
                        {
                            { "Відповідь", "В окремих випадках." },
                            { "Оцінка", 3 },
                        }
                    },
                    new FrameRow
                    {
                        Slots = new Dictionary<string, object>
                        {
                            { "Відповідь", "Не потрібні." },
                            { "Оцінка", 2 },
                        }
                    }
                }
            };
            Frame q7 = new Frame
            {
                Name = "Чи відчуваєте ви загальний контекст вирішення задачі?",
                Rows = new List<FrameRow>
                {
                    new FrameRow
                    {
                        Slots = new Dictionary<string, object>
                        {
                            { "Відповідь", "Так." },
                            { "Оцінка", 2 },
                        }
                    },
                    new FrameRow
                    {
                        Slots = new Dictionary<string, object>
                        {
                            { "Відповідь", "Частково." },
                            { "Оцінка", 3 },
                        }
                    },
                    new FrameRow
                    {
                        Slots = new Dictionary<string, object>
                        {
                            { "Відповідь", "В окремих випадках." },
                            { "Оцінка", 5 },
                        }
                    }
                }
            };

            Frame q8 = new Frame
            {
                Name = "Чи можете ви побудувати модель вирішуваної задачі?",
                Rows = new List<FrameRow>
                {
                    new FrameRow
                    {
                        Slots = new Dictionary<string, object>
                        {
                            { "Відповідь", "Так." },
                            { "Оцінка", 5 },
                        }
                    },
                    new FrameRow
                    {
                        Slots = new Dictionary<string, object>
                        {
                            { "Відповідь", "Не повністю." },
                            { "Оцінка", 3 },
                        }
                    },
                    new FrameRow
                    {
                        Slots = new Dictionary<string, object>
                        {
                            { "Відповідь", "В окремих випадках." },
                            { "Оцінка", 2 },
                        }
                    }
                }
            };
            Frame q9 = new Frame
            {
                Name = "Чи вистачає вам ініціативи при вирішенні задач?",
                Rows = new List<FrameRow>
                {
                    new FrameRow
                    {
                        Slots = new Dictionary<string, object>
                        {
                            { "Відповідь", "Так." },
                            { "Оцінка", 5 },
                        }
                    },
                    new FrameRow
                    {
                        Slots = new Dictionary<string, object>
                        {
                            { "Відповідь", "Зрідка." },
                            { "Оцінка", 3 },
                        }
                    },
                    new FrameRow
                    {
                        Slots = new Dictionary<string, object>
                        {
                            { "Відповідь", "Потрібне натхнення." },
                            { "Оцінка", 2 },
                        }
                    }
                }
            };
            Frame q10 = new Frame
            {
                Name = "Чи можете вирішувати проблеми, з якими ще не стикались?",
                Rows = new List<FrameRow>
                {
                    new FrameRow
                    {
                        Slots = new Dictionary<string, object>
                        {
                            { "Відповідь", "Так." },
                            { "Оцінка", 5 },
                        }
                    },
                    new FrameRow
                    {
                        Slots = new Dictionary<string, object>
                        {
                            { "Відповідь", "В окремих випадках." },
                            { "Оцінка", 3 },
                        }
                    },
                    new FrameRow
                    {
                        Slots = new Dictionary<string, object>
                        {
                            { "Відповідь", "Ні." },
                            { "Оцінка", 2 },
                        }
                    }
                }
            };

            Frame q11 = new Frame
            {
                Name = "Чи необхідний вам весь контекст задачі?",
                Rows = new List<FrameRow>
                {
                    new FrameRow
                    {
                        Slots = new Dictionary<string, object>
                        {
                            { "Відповідь", "Так." },
                            { "Оцінка", 5 },
                        }
                    },
                    new FrameRow
                    {
                        Slots = new Dictionary<string, object>
                        {
                            { "Відповідь", "В окремих деталях." },
                            { "Оцінка", 3 },
                        }
                    },
                    new FrameRow
                    {
                        Slots = new Dictionary<string, object>
                        {
                            { "Відповідь", "В загальному." },
                            { "Оцінка", 2 },
                        }
                    }
                }
            };
            Frame q12 = new Frame
            {
                Name = "Чи переглядаєте ви свої наміри до вирішення задачі?",
                Rows = new List<FrameRow>
                {
                    new FrameRow
                    {
                        Slots = new Dictionary<string, object>
                        {
                            { "Відповідь", "Так." },
                            { "Оцінка", 5 },
                        }
                    },
                    new FrameRow
                    {
                        Slots = new Dictionary<string, object>
                        {
                            { "Відповідь", "Зрідка." },
                            { "Оцінка", 3 },
                        }
                    },
                    new FrameRow
                    {
                        Slots = new Dictionary<string, object>
                        {
                            { "Відповідь", "Коли є потреба." },
                            { "Оцінка", 2 },
                        }
                    }
                }
            };
            Frame q13 = new Frame
            {
                Name = "Чи здатні  ви  навчатись у інших?",
                Rows = new List<FrameRow>
                {
                    new FrameRow
                    {
                        Slots = new Dictionary<string, object>
                        {
                            { "Відповідь", "Так." },
                            { "Оцінка", 5 },
                        }
                    },
                    new FrameRow
                    {
                        Slots = new Dictionary<string, object>
                        {
                            { "Відповідь", "Зрідка." },
                            { "Оцінка", 3 },
                        }
                    },
                    new FrameRow
                    {
                        Slots = new Dictionary<string, object>
                        {
                            { "Відповідь", "Коли є потреба." },
                            { "Оцінка", 2 },
                        }
                    }
                }
            };

            Frame q14 = new Frame
            {
                Name = "Чи обираєте ви нові методи своєї роботи?",
                Rows = new List<FrameRow>
                {
                    new FrameRow
                    {
                        Slots = new Dictionary<string, object>
                        {
                            { "Відповідь", "Так." },
                            { "Оцінка", 5 },
                        }
                    },
                    new FrameRow
                    {
                        Slots = new Dictionary<string, object>
                        {
                            { "Відповідь", "Вибірково." },
                            { "Оцінка", 3 },
                        }
                    },
                    new FrameRow
                    {
                        Slots = new Dictionary<string, object>
                        {
                            { "Відповідь", "Вистачає досвіду." },
                            { "Оцінка", 2 },
                        }
                    }
                }
            };
            Frame q15 = new Frame
            {
                Name = "Чи допомагає власна інтуїція при вирішенні задач?",
                Rows = new List<FrameRow>
                {
                    new FrameRow
                    {
                        Slots = new Dictionary<string, object>
                        {
                            { "Відповідь", "Так." },
                            { "Оцінка", 5 },
                        }
                    },
                    new FrameRow
                    {
                        Slots = new Dictionary<string, object>
                        {
                            { "Відповідь", "Частково." },
                            { "Оцінка", 3 },
                        }
                    },
                    new FrameRow
                    {
                        Slots = new Dictionary<string, object>
                        {
                            { "Відповідь", "При емоційному напруженні." },
                            { "Оцінка", 2 },
                        }
                    }
                }
            };
            Frame q16 = new Frame
            {
                Name = "Чи застовуєте рішення задач за аналогією?",
                Rows = new List<FrameRow>
                {
                    new FrameRow
                    {
                        Slots = new Dictionary<string, object>
                        {
                            { "Відповідь", "Часто." },
                            { "Оцінка", 5 },
                        }
                    },
                    new FrameRow
                    {
                        Slots = new Dictionary<string, object>
                        {
                            { "Відповідь", "Зрідка." },
                            { "Оцінка", 3 },
                        }
                    },
                    new FrameRow
                    {
                        Slots = new Dictionary<string, object>
                        {
                            { "Відповідь", "Тільки власний варіант." },
                            { "Оцінка", 2 },
                        }
                    }
                }
            };

            Frame FrameNovice = new Frame
            {
                Name = "Початківець",
                Rows = new List<FrameRow>
                {
                    new FrameRow
                    {
                        Slots = new Dictionary<string, object>
                        {
                            { "Питання 1", q1 },
                            { "Питання 2", q2 },
                            { "Питання 3", q3 },
                            { "Питання 4", q4 }
                        }
                    }
                }
            };
            Frame FrameAdvanced = new Frame
            {
                Name = "Твердий початківець",
                Rows = new List<FrameRow>
                {
                    new FrameRow
                    {
                        Slots = new Dictionary<string, object>
                        {
                            { "Питання 1", q5 },
                            { "Питання 2", q6 },
                            { "Питання 3", q7 }
                        }
                    }
                }
            };
            Frame FrameCompetent = new Frame
            {
                Name = "Компетентний",
                Rows = new List<FrameRow>
                {
                    new FrameRow
                    {
                        Slots = new Dictionary<string, object>
                        {
                            { "Питання 1", q8 },
                            { "Питання 2", q9 },
                            { "Питання 3", q10 }
                        }
                    }
                }
            };
            Frame FrameProficient = new Frame
            {
                Name = "Досвідчений",
                Rows = new List<FrameRow>
                {
                    new FrameRow
                    {
                        Slots = new Dictionary<string, object>
                        {
                            { "Питання 1", q11 },
                            { "Питання 2", q12 },
                            { "Питання 3", q13 }
                        }
                    }
                }
            };
            Frame FrameExpert = new Frame
            {
                Name = "Експерт",
                Rows = new List<FrameRow>
                {
                    new FrameRow
                    {
                        Slots = new Dictionary<string, object>
                        {
                            { "Питання 1", q14 },
                            { "Питання 2", q15 },
                            { "Питання 3", q16 }
                        }
                    }
                }
            };
            Frame Questioning = new Frame
            {
                Name = "Опитування",
                Rows = new List<FrameRow>
                {
                    new FrameRow
                    {
                        Slots = new Dictionary<string, object>
                        {
                            { "Блок питань 1", FrameNovice },
                            { "Блок питань 2", FrameAdvanced },
                            { "Блок питань 3", FrameCompetent },
                            { "Блок питань 4", FrameProficient },
                            { "Блок питань 5", FrameExpert }
                        }
                    }
                }
            };
            return Questioning;
        }
    }
}