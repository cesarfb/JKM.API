using JKM.PERSISTENCE.Utils;
using System.Threading.Tasks;

namespace JKM.PERSISTENCE.Repository.Notification
{
    public interface INotificationRepository
    {
        Task ContactUs(NotificationModel notification);
    }
}
