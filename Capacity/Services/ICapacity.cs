using bigmojo.net.capacity.api.Model;

namespace bigmojo.net.capacity.Interface {

    public interface ICapacity {
        int[] GetCapacity (int startWeek, int numberOfWeeks, Project project);
    }
}