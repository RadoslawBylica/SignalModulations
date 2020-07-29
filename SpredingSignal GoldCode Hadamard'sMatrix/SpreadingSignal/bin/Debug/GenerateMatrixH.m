function [H] = GenerateMatrixH(Size)
    HBase = [0 0; 0 1];
    if Size <= 0
        H = -1;
    elseif Size == 1
        H = HBase;
    else
        H = [HBase HBase; HBase, ~HBase];
        for i = 3:Size
          H = [H H; H ~H];
        end   
    end
end