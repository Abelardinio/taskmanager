import * as _ from 'lodash';
import { Utils } from '../common/utils';

describe('Utils methods tests', () => {

  it('getDateTimeRange should calculate date time range', () => {
      const startDate = new Date(2018, 9, 21, 9, 10, 10);
      const endDate = new Date(2018, 9, 23, 10, 40, 40);
      const range = Utils.getDateTimeRange(startDate, endDate);
      expect(range.days).toBe(2);
      expect(range.hours).toBe(1);
      expect(range.minutes).toBe(30);
      expect(range.seconds).toBe(30);
  });

  it('generateArray should generate an array of numbers', () => {
    const array = Utils.generateArray(3);
    expect(array.length).toBe(3);
    _.forEach(array, (number, index) => {
      expect(number).toBe(index);
    });
  });
});
