import { PagingInfo } from './PagingInfo';
import { BaseFilter } from './BaseFilter';

export class FeatureFilter extends BaseFilter {
  constructor(
    public PagingInfo: PagingInfo,
    public Value?: string
  ) {
    super(PagingInfo);
  }
}
