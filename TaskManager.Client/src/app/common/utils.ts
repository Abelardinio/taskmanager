import * as _ from 'lodash';
import { DateTimeRange } from 'src/app/models/DateTimeRange';

export class Utils {

    /**
     * Generate an array of numbers of size n
     *
     * @param n size of an array
     */
    public static generateArray(n: number): Array<number> {
        return  _.times(n);
    }

    /**
     * Returns an amount of time between two dates
     *
     * @param startDate start date
     * @param endDate ends date
     */
    public static getDateTimeRange(startDate: Date, endDate: Date): DateTimeRange {
        const distance = endDate.getTime() - startDate.getTime();

        if (distance < 0) {
          return null;
        }

        const days = Math.floor(distance / (1000 * 60 * 60 * 24));
        const hours = Math.floor((distance % (1000 * 60 * 60 * 24)) / (1000 * 60 * 60));
        const minutes = Math.floor((distance % (1000 * 60 * 60)) / (1000 * 60));
        const seconds = Math.floor((distance % (1000 * 60)) / 1000);

        return new DateTimeRange(days, hours, minutes, seconds);
    }

    /**
     * Returns true if input array are equal
     *
     * @param array1 first array
     * @param array2 second array
     */
    public static equals(array1: number[], array2: number[]): boolean {
        return array1.length === array2.length && array1.every(function (value, index) { return value === array2[index]; });
    }
}
